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
            var whatsonData = ReadWhatsonData(protocol);
            var mediatorData = await ReadMediatorData(protocol);

            var mergedRows = Merger.Merge(adSalesData, whatsonData, mediatorData);
            PublishMergedTable(protocol, mergedRows);
        } catch(Exception e)
        {
            protocol.Mergeddebugmsg = $"Exception {e.Message}";
        }

    }

    public List<object[]> PublishAdsalesTable(SLProtocolExt protocol, List<Utils.AdSalesRow> adSalesData) {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in adSalesData)
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

    public List<object[]> PublishMergedTable(SLProtocolExt protocol, MergedEntry[] mergedRows)
    {
        List<object[]> tableRows = new List<object[]>();
        foreach (var row in mergedRows)
        {
            tableRows.Add(new MergedtableQActionRow
            {
                Mergedreconcilekey = row.adSalesData.ContentReconcileKey,
                Mergedproductcode = row.adSalesData.ContentProductCode,
                Breaktime = row.adSalesBreakData.BreakNominalTime,
                Mergedduration = row.adSalesData.ContentTotalDuration,
                Mergedhavewon = (row.whatsonData != null) ? "\u2713" : string.Empty,
                Mergedhavemediator = (row.mediatorData != null) ? "✓" : string.Empty,
                Mergedwontime = (row.whatsonData != null) ? row.whatsonData.StartDate.ToShortTimeString() : string.Empty,
                Mergedmediatortime = (row.mediatorData != null) ? row.mediatorData.StartDateTime.ISO8601().ToString() : string.Empty,
            }.ToObjectArray());
        }
        protocol.FillArray(Parameter.Mergedtable.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        return tableRows;
    }

    public static Pharos ReadWhatsonData(SLProtocolExt protocol)
    {
        string channelName = (string)protocol.GetParameter(Parameter.channelnameadsales);
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
        string channelName = (string)protocol.GetParameter(Parameter.channelnameadsales);
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
            string channelName = (string)protocol.GetParameter(Parameter.channelnameadsales);
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
