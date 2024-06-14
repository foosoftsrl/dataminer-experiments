using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
        protocol.Mergediterationcounter = (double)protocol.Mergediterationcounter + 2;
        try
        {
            var adSalesData = ReadAdSalesData(protocol).flatten();
            PublishAdsalesTable(protocol, adSalesData);
            var whatsonData = ReadWhatsonData(protocol).flatten();
            PublishWhatsonTable(protocol, whatsonData);
            var mediatorData = (await ReadMediatorData(protocol)).flatten();
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
        protocol.Wondebugmsg = string.Empty;
        return tableRows;
    }

    public List<object[]> PublishMediatorTable(SLProtocolExt protocol, List<Utils.MediatorRow> mediatorRows)
    {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in mediatorRows)
        {
            tableRows.Add(new MediatorQActionRow
            {
                Mediatorid = row.row.ScheduleReference.GenericList.Object[0],
                Mediatordate = row.row.startDateTime()?.ToString("yyyy-MM-dd HH:mm:ss"),
                Mediatortitle = row.row.Title.AsString(),
                Mediatorreconcilekey = row.ReconcileKey

            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Mediator.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        protocol.Mediatordebugmsg = $"{tableRows.Count}";
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
                Mergedmediatortime = (row.mediatorData != null) ? row.mediatorData.startDateTime()?.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Mergedtable.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        return tableRows;
    }

    public static Pharos ReadWhatsonData(SLProtocolExt protocol)
    {
        string channelName = (string)protocol.GetParameter(Parameter.channelname);
        string dir = @"\\winfs01.mediaset.it\DM_Watchfolder\WON";
        try
        {
            return whatsonSource.ReadWhatson(channelName, dir);
        }
        catch (Exception ex)
        {
            protocol.Wondebugmsg = "Failed reading Whatson data";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }

    public static AdSales.DataType ReadAdSalesData(SLProtocolExt protocol)
    {
        string channelName = (string)protocol.GetParameter(Parameter.channelname);
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

    public static async Task<Mediator.Rootobject> ReadMediatorData(SLProtocolExt protocol)
    {
        try
        {
            string uri = (string)protocol.GetParameter(Parameter.urimediator);
            string channelName = (string)protocol.GetParameter(Parameter.channelname);
            int maxResults = Convert.ToInt32(protocol.GetParameter(Parameter.maxresultsmediator));
            return await mediatorSource.ReadMediator(uri, channelName, maxResults);
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }
}
