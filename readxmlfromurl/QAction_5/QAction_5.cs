using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
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
    private static AdSalesSource adSalesSource = new AdSalesSource();
    private static MediatorSource mediatorSource = new MediatorSource();
    private static WhatsonSource whatsonSource = new WhatsonSource();

    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public async Task Run(SLProtocolExt protocol)
    {
        protocol.Mergediterationcounter = (double)protocol.Mergediterationcounter + 1;
        try
        {
            var adSalesData = ReadAdSalesData(protocol);
            PublishAdsalesTable(protocol, adSalesData);
            var whatsonData = ReadWhatsonData(protocol);
            PublishWhatsonTable(protocol, whatsonData);
            var mediatorData = await ReadMediatorData(protocol);
            PublishMediatorTable(protocol, mediatorData);
            await ReadEnablerLegacy(protocol);
            await ReadEnablerScte(protocol);

            var mergedRows = Merger.Merge(adSalesData, whatsonData, mediatorData);
            PublishMergedTable(protocol, mergedRows);
            protocol.Mergeddebugmsg = $"Everything ok!";
        }
        catch (Exception e)
        {
            protocol.Mergeddebugmsg = $"Exception {e.Message}";
        }

    }

    public List<object[]> PublishAdsalesTable(SLProtocolExt protocol, List<AdSalesRow> adSalesRows) {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in adSalesRows)
        {
            tableRows.Add(new AdsalesQActionRow
            {
                Adsalestime = row.TimeOfDay,
                Adsalesbreakid = row.BreakId,
                Adsalesreconcilekey = row.ReconcileKey,
                Adsalestitle = row.Title,
                Adsalestype = row.Type,
                Adsalesenabler = row.Enabler,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Adsales.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        protocol.Adsalesdebugmsg = "";
        return tableRows;
    }

    public List<object[]> PublishWhatsonTable(SLProtocolExt protocol, List<WhatsonRow> whatsonRows)
    {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in whatsonRows)
        {
            tableRows.Add(new WonQActionRow
            {
                Wonstartdate = row.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                Wontitle = row.Title,
                Wonreconcilekey = row.ReconcileKey ?? string.Empty,
                Wonitemreference = row.ItemReference,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Won.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        return tableRows;
    }

    public List<object[]> PublishMediatorTable(SLProtocolExt protocol, List<MediatorRow> mediatorRows)
    {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in mediatorRows)
        {
            tableRows.Add(new MediatorQActionRow
            {
                Mediatorid = row.Id,
                Mediatorschedulereference = row.ScheduleReference,
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
                Mergedreconcilekey = row.adSalesData.ReconcileKey,
                Mergedproductcode = row.adSalesData.ProductCode,
                Mergedduration = row.adSalesData.Duration,
                Mergedadsalestime = row.adSalesTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Mergedhavewon = (row.whatsonData != null) ? "\u2713" : string.Empty,
                Mergedhavemediator = (row.mediatorData != null) ? "✓" : string.Empty,
                Mergedwontime = row.whatsonData?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                Mergedmediatortime = row.mediatorData?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                Mergedtype = row.adSalesData.Type,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Mergedtable.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        return tableRows;
    }
    public async Task<string> ReadEnablerLegacy(SLProtocolExt protocol)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{protocol.Probeurl}legacy?channel={protocol.channelName()}"),
                };

                using (var response = await httpClient.SendAsync(request))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    protocol.Legacydebugmsg = "Everything ok!";
                    var rows = apiResponse.Split('\n');
                    List<object[]> tableRows = new List<object[]>();
                    foreach (var row in rows)
                    {
                        var cells = row.Split(',');
                        tableRows.Add(new EnablerlegacyQActionRow
                        {
                            Enablerlegacytime = cells[0],
                            Enablerlegacyeventcode = cells.Length > 1 ? cells[1] : string.Empty,
                            Enablerlegacyeventname = cells.Length > 2 ? cells[2] : string.Empty,
                            Enablerlegacypayload = cells.Length > 3 ? cells[3] : string.Empty,
                        }.ToObjectArray());
                    }
                    protocol.FillArray(Parameter.Enablerlegacy.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
                    return apiResponse;
                }
            }
        } catch(Exception ex)
        {
            protocol.Legacydebugmsg = $"Exception {ex.Message}";
            return "";
        }
    }

    public async Task<string> ReadEnablerScte(SLProtocolExt protocol)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{protocol.Probeurl}scte?channel={protocol.channelName()}"),
                };

                using (var response = await httpClient.SendAsync(request))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    protocol.Legacydebugmsg = "Everything ok!";
                    var rows = apiResponse.Split('\n');
                    List<object[]> tableRows = new List<object[]>();
                    foreach (var row in rows)
                    {
                        var cells = row.Split(',');
                        tableRows.Add(new EnablerscteQActionRow
                        {
                            Enablersctetime = cells[0],
                            Enablerscteeventcode = cells.Length > 1 ? cells[1] : string.Empty,
                            Enablerscteeventname = cells.Length > 2 ? cells[2] : string.Empty,
                            Enablersctepayload = cells.Length > 3 ? cells[3] : string.Empty,
                        }.ToObjectArray());
                    }
                    protocol.FillArray(Parameter.Enablerscte.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
                    return apiResponse;
                }
            }
        }
        catch (Exception ex)
        {
            protocol.Legacydebugmsg = $"Exception {ex.Message}";
            return "";
        }
    }

    public static List<WhatsonRow> ReadWhatsonData(SLProtocolExt protocol)
    {
        string channelName = (string)protocol.channelName();
        string dir = @"\\winfs01.mediaset.it\DM_Watchfolder\WON";
        try
        {
            var rows = whatsonSource.ReadWhatson(channelName, dir);
            protocol.Wondebugmsg = $"Read {rows.Count} columns";
            return rows;
        }
        catch (Exception ex)
        {
            protocol.Wondebugmsg = $"Failed reading Whatson data: {ex.Message}";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw new Exception("Failed reading Whatson data", ex);
        }
    }

    public static List<AdSalesRow> ReadAdSalesData(SLProtocolExt protocol)
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

    public List<MediatorRow> GetLastPublishedMediator(SLProtocolExt protocol)
    {
        var result = new List<MediatorRow>();
        try
        {
            var count = protocol.mediator.RowCount;
            for(var idx = 0; idx < count; idx++) 
            {
                var data = (object[])protocol.GetRow(Parameter.Mediator.tablePid, idx);
                var row = new MediatorQActionRow(data);
                result.Add(new MediatorRow
                {
                    Id = Int32.Parse((string)row.Mediatorid),
                    StartTime = DateTime.Parse((string)row.Mediatordate),
                    ReconcileKey = (string)row.Mediatorreconcilekey,
                    Title = (string)row.Mediatortitle,
                    Status = (string)row.Mediatorstatus,
                    ScheduleReference = (string)row.Mediatorschedulereference,
                });
            }
        } catch(Exception e)
        {
            protocol.Mediatordebugmsg = $"Failed parsing mediator table";
        }
        return result;
    }
    public async Task<List<MediatorRow>> ReadMediatorData(SLProtocolExt protocol)
    {
        try
        {
            string uri = (string)protocol.GetParameter(Parameter.urimediator);
            string channelName = protocol.channelName();
            int maxResults = Convert.ToInt32(protocol.GetParameter(Parameter.maxresultsmediator));
            var lastPublished = GetLastPublishedMediator(protocol);
            var parsed = await mediatorSource.ReadMediator(uri, channelName, maxResults);
            var merged = mediatorSource.Merge(lastPublished, parsed);
            protocol.Mediatordebugmsg = $"State {lastPublished.Count}, Parsed {parsed.Count} Merged {merged.Count} lines";
            return lastPublished;
        }
        catch (Exception ex)
        {
            protocol.Mediatordebugmsg = $"Failed reading Mediator data: {ex.Message}";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }
}
