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
    private AdSalesSource adSalesSource = new AdSalesSource();
    private MediatorSource mediatorSource = new MediatorSource();
    private WhatsonSource whatsonSource = new WhatsonSource();
    private EnablerSource enablerSource = new EnablerSource();

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
            protocol.PublishAdsalesTable(adSalesData);
            var whatsonData = ReadWhatsonData(protocol);
            protocol.PublishWhatsonTable(whatsonData);
            var mediatorData = await ReadMediatorData(protocol);
            protocol.PublishMediatorTable(mediatorData);
            var legacy = await ReadEnablerLegacy(protocol);
            protocol.PublishEnablerLegacyTable(legacy);
            var scte = await ReadEnablerScte(protocol);
            protocol.PublishScteTable(scte);
            var mergedRows = Merger.Merge(adSalesData, whatsonData, mediatorData, scte, legacy);
            protocol.PublishMergedTable(mergedRows);
            protocol.PublishXPrintTable(adSalesData, whatsonData, mediatorData);

            var xprintDiff = DiffTool.ComputeDiff(adSalesData, whatsonData.FilterSpots());
            protocol.PublishXPrintDiffTable(xprintDiff);
            protocol.Mergeddebugmsg = $"Everything ok!";
        }
        catch (Exception e)
        {
            protocol.Mergeddebugmsg = $"Exception {e.Message} ${e.StackTrace.Substring(0,200)}";
        }

    }


    public async Task<List<EnablerRow>> ReadEnablerLegacy(SLProtocolExt protocol)
    {
        try
        {
            var url = $"{protocol.Probeurl}legacy?channel={protocol.channelName()}";
            var rows = await enablerSource.ReadEnabler(url);
            protocol.Legacydebugmsg = "Everything ok...";
            return rows;
        }
        catch (Exception ex)
        {
            protocol.Legacydebugmsg = $"Exception {ex.Message}";
            return new List<EnablerRow>();
        }
    }

    public async Task<List<EnablerRow>> ReadEnablerScte(SLProtocolExt protocol)
    {
        try
        {
            var url = $"{protocol.Probeurl}scte?channel={protocol.channelName()}";
            var rows = await enablerSource.ReadEnabler(url);
            protocol.Sctedebugmsg = "Everything ok...";
            return rows;
        }
        catch (Exception ex)
        {
            protocol.Sctedebugmsg = $"Exception {ex.Message}";
            return new List<EnablerRow>();
        }
    }

    public List<WhatsonRow> ReadWhatsonData(SLProtocolExt protocol)
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

    public List<AdSalesRow> ReadAdSalesData(SLProtocolExt protocol)
    {
        string channelName = protocol.channelName();
        string dir = @"\\winfs01.mediaset.it\DM_Watchfolder\Adsales";
        try
        {
            var rows = adSalesSource.ReadAdSales(channelName, dir);
            protocol.Adsalesdebugmsg = "Everything ok";
            return rows;
        }
        catch (Exception ex)
        {
            protocol.Adsalesdebugmsg = "Failed parsing AdSales data";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }

    public string NullIfEmpty(string s)
    {
        if (s.Length == 0)
            return null;
        return s;
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
                    ReconcileKey = NullIfEmpty((string)row.Mediatorreconcilekey),
                    Title = NullIfEmpty((string)row.Mediatortitle),
                    Status = NullIfEmpty((string)row.Mediatorstatus),
                    ScheduleReference = NullIfEmpty((string)row.Mediatorschedulereference),
                });
            }
        } catch(Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
        return result;
    }

    public async Task<List<MediatorRow>> ReadMediatorData(SLProtocolExt protocol)
    {
        var lastPublished = GetLastPublishedMediator(protocol);
        try
        {
            string uri = protocol.GetRequiredNonEmptyStringParameter(Parameter.urimediator);
            string channelName = protocol.channelName();
            int maxResults = protocol.GetRequiredIntParameter(Parameter.maxresultsmediator);
            var parsed = await mediatorSource.ReadMediator(uri, channelName, maxResults);
            var merged = mediatorSource.Merge(lastPublished, parsed);
            protocol.Mediatordebugmsg = $"State {lastPublished.Count}, Parsed {parsed.Count} Merged {merged.Count} lines";
            return merged;
        }
        catch (Exception ex)
        {
            protocol.Mediatordebugmsg = $"Failed reading Mediator data: {ex.Message}";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            return lastPublished;
        }
    }
}
