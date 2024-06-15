using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Mediator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QAction_5;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public class QAction
{
    private static IAdSalesSource adSalesSource = new AdSalesSource();
    private static IMediatorSource mediatorSource = new MediatorSource();
    private static IWhatsonSource whatsonSource = new WhatsonSource();
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public async Task Run(SLProtocolExt protocol)
    {
        protocol.Mergediterationcounter = (double)protocol.Mergediterationcounter + 1;
        try
        {
            var adSalesData = ReadAdSalesData(protocol).flatten();
            PublishAdsalesTable(protocol, adSalesData);
            var whatsonData = ReadWhatsonData(protocol);
            PublishWhatsonTable(protocol, whatsonData);
            var mediatorData = await ReadMediatorData(protocol);
            PublishMediatorTable(protocol, mediatorData);

            var mergedRows = Merger.Merge(adSalesData, whatsonData, mediatorData);
            PublishMergedTable(protocol, mergedRows);
            protocol.Mergeddebugmsg = $"Everything ok!";
        }
        catch (Exception e)
        {
            protocol.Mergeddebugmsg = $"Exception {e.Message}";
        }

    }

    public List<object[]> PublishAdsalesTable(SLProtocolExt protocol, List<Utils.AdSalesRow> adSalesRows) {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in adSalesRows)
        {
            tableRows.Add(new AdsalesQActionRow
            {
                Adsalestime = row.TimeOfDay,
                Adsalesreconcilekey = row.ReconcileKey,
                Adsalestitle = row.Content.ContentBrand,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Adsales.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        protocol.Adsalesdebugmsg = "";
        return tableRows;
    }

    public List<object[]> PublishWhatsonTable(SLProtocolExt protocol, List<Utils.WhatsonRow> whatsonRows)
    {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in whatsonRows)
        {
            tableRows.Add(new WonQActionRow
            {
                Wonstartdate = row.PlaylistItem.startDateTime()?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                Wontitle = row.PlaylistItem.ScheduledTitle,
                Wonreconcilekey = row.ReconcileKey ?? string.Empty,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Won.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        return tableRows;
    }

    public List<object[]> PublishMediatorTable(SLProtocolExt protocol, List<Utils.MediatorRow> mediatorRows)
    {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in mediatorRows)
        {
            tableRows.Add(new MediatorQActionRow
            {
                Mediatorid = row.ScheduleReference,
                Mediatordate = row.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Mediatortitle = row.Title,
                Mediatorstatus = row.Status,
                Mediatorreconcilekey = row.ReconcileKey,

            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Mediator.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        return tableRows;
    }

    public List<object[]> PublishMergedTable(SLProtocolExt protocol, MergedEntry[] mergedRows)
    {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in mergedRows)
        {
            tableRows.Add(new MergedtableQActionRow
            {
                Mergedreconcilekey = row.adSalesData.ContentReconcileKey,
                Mergedproductcode = row.adSalesData.ContentProductCode,
                Mergedadsalestime = row.adSalesTime,
                Mergedduration = row.adSalesData.ContentTotalDuration,
                Mergedhavewon = (row.whatsonData != null) ? "\u2713" : string.Empty,
                Mergedhavemediator = (row.mediatorData != null) ? "✓" : string.Empty,
                Mergedwontime = row.whatsonData?.Time ?? string.Empty,
                Mergedmediatortime = row.mediatorData?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Mergedtable.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        return tableRows;
    }

    public static List<Utils.WhatsonRow> ReadWhatsonData(SLProtocolExt protocol)
    {
        string channelName = (string)protocol.channelName();
        string dir = @"\\winfs01.mediaset.it\DM_Watchfolder\WON";
        try
        {
            var data = whatsonSource.ReadWhatson(channelName, dir);
            if (data == null)
            {
                protocol.Wondebugmsg = $"No whatson data for channel {channelName}";
                return new List<Utils.WhatsonRow>();
            }
            else {
                var rows = data.flatten();
                protocol.Wondebugmsg = $"Read {rows.Count} columns";
                return rows;
            }
        }
        catch (Exception ex)
        {
            protocol.Wondebugmsg = $"Failed reading Whatson data: {ex.Message}";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw new Exception("Failed reading Whatson data", ex);
        }
    }

    public static AdSales.DataType ReadAdSalesData(SLProtocolExt protocol)
    {
        string channelName = protocol.channelName();
        string dir = @"\\winfs01.mediaset.it\DM_Watchfolder\Adsales";
        try
        {
            return adSalesSource.ReadAdSales(channelName, dir);
        }
        catch (Exception ex)
        {
            protocol.Adsalesdebugmsg = "Failed parsing AdSales data";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }

    public List<Utils.MediatorRow> GetLastPublishedMediator(SLProtocolExt protocol)
    {
        var idx = 0;
        var result = new List<Utils.MediatorRow>();
        while(true)
        {
            var data = (object[])protocol.GetRow(Parameter.Mediator.tablePid, idx++);
            var row = new MediatorQActionRow(data);
            if (row.Mediatordate == null)
                break;
            result.Add(new Utils.MediatorRow
            {
                StartTime = DateTime.Parse((string)row.Mediatordate),
                ReconcileKey = (string)row.Mediatorreconcilekey,
                Title = (string)row.Mediatortitle,
                Status = (string)row.Mediatorstatus,
                ScheduleReference=(string)row.Mediatorid,
            });
        }
        return result;
    }
    public async Task<List<Utils.MediatorRow>> ReadMediatorData(SLProtocolExt protocol)
    {
        try
        {
            string uri = (string)protocol.GetParameter(Parameter.urimediator);
            string channelName = protocol.channelName();
            int maxResults = Convert.ToInt32(protocol.GetParameter(Parameter.maxresultsmediator));
            var obj = await mediatorSource.ReadMediator(uri, channelName, maxResults);
            var parsedList = (obj != null) ? obj.flatten() : new List<Utils.MediatorRow>();
            var merged = new List<Utils.MediatorRow>();
            DateTime? firstMemoryTimeStamp = null;
            var lastPublishedMediator = GetLastPublishedMediator(protocol);
            if (lastPublishedMediator.Count != 0)
                firstMemoryTimeStamp = lastPublishedMediator.First().StartTime;
            DateTime? firstParsedTimeStamp = null;
            if(parsedList.Count != 0)
                firstParsedTimeStamp = parsedList.First().StartTime;
            if (parsedList.Count != 0)
            {
                var first = parsedList.First();
                foreach(var row in lastPublishedMediator)
                {
                    if (row.StartTime >= first.StartTime)
                        break;
                    merged.Add(row);
                }
            }
            merged.AddRange(parsedList);
            lastPublishedMediator = merged;
            protocol.Mediatordebugmsg = $"Parsed {parsedList.Count}-{firstMemoryTimeStamp?.ToString() ?? ""}, State {lastPublishedMediator.Count}-{firstParsedTimeStamp?.ToString() ?? ""} Produced {merged.Count} lines";
            return merged;
        }
        catch (Exception ex)
        {
            protocol.Mediatordebugmsg = $"Failed reading Mediator data: {ex.Message}";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }
}
