﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Mediator;
using Newtonsoft.Json;

public static class Utils
{
    public class AdSalesRow
    {
        public string TimeOfDay;
        public AdSales.ContentType Content;
        public AdSales.BreakType Break;
        public string ReconcileKey;
    }

    public class WhatsonRow
    {
        public PharosPlaylistBlock Block;
        public PharosPlaylistBlockPlaylistItem PlaylistItem;
        public string ReconcileKey;
    }

    public static List<AdSalesRow> flatten(this AdSales.DataType adSalesData)
    {
        List<AdSalesRow> result = new List<AdSalesRow>();
        foreach (var break_ in adSalesData.Breaks)
        {
            var timeOfDay = TimeSpan.ParseExact(break_.BreakNominalTime, @"mm\:ss", null).TotalSeconds * 60;
            foreach (var timeAllocation in break_.TimeAllocations)
            {
                foreach (var content in timeAllocation.Contents)
                {
                    TimeSpan t = TimeSpan.FromSeconds(timeOfDay);
                    result.Add(new AdSalesRow
                    {
                        TimeOfDay = t.TotalHours.ToString("00") + t.ToString(@"\:mm\:ss"),
                        Content = content,
                        Break = break_,
                        ReconcileKey = content.ContentReconcileKey,
                    });
                    timeOfDay += (content.ContentTotalDuration != null) ? Int32.Parse(content.ContentTotalDuration) : 0;
                }
            }
        }
        return result;
    }

    public static List<WhatsonRow> flatten(this Pharos whatsonData)
    {
        List<WhatsonRow> result = new List<WhatsonRow>();
        foreach (var blockList in whatsonData.Playlist.BlockList)
        {
            foreach (var playlistItem in blockList.PlaylistItem)
            {
                var reconcileKey = playlistItem.findAdSalesReconcileKey();
                result.Add(new WhatsonRow
                {
                    Block = blockList,
                    PlaylistItem = playlistItem,
                    ReconcileKey = reconcileKey
                });
            }
        }
        return result;
    }

    public static Dictionary<String, PharosPlaylistBlockPlaylistItem> toReconcileKeyMap(this List<WhatsonRow> whatsonRows)
    {
        Dictionary<String, PharosPlaylistBlockPlaylistItem> reconcileToRow = new Dictionary<String, PharosPlaylistBlockPlaylistItem>();
        foreach(var row in whatsonRows)
        {
            var adSalesReconcileKey = row.ReconcileKey;
            if (adSalesReconcileKey != null)
            {
                reconcileToRow.Add(adSalesReconcileKey, row.PlaylistItem);
            }
        }
        return reconcileToRow;
    }

    public static string findAdSalesReconcileKey(this PharosPlaylistBlockPlaylistItem playlistItem)
    {
        foreach(var entry in playlistItem.Template.DataElementList)
        {
            if(entry.Name == "adSalesContentReconcileKey-text")
            {
                if (entry.Value.Text != null && entry.Value.Text.Length == 1)
                {
                    return entry.Value.Text[0];
                }
            }
        }
        return null;
    }
    public static string findAdSalesReconcileKey(this Mediator.Row mediatorRow)
    {
        foreach (var entry in mediatorRow.TemplateParameterList.GenericList.Object)
        {
            foreach (var templateParameter in entry.TemplateParameter)
            {
                if (templateParameter.Name == "adSalesContentReconcileKey-text")
                {
                    if (templateParameter.Value is string)
                    {
                        return (string)templateParameter.Value;
                    }
                }
            }
        }
        return null;
    }

    public static Dictionary<string, Mediator.Row> toReconcileKeyMap(this Mediator.Rootobject rootObjects)
    {
        Dictionary<String, Mediator.Row> reconcileToRow = new Dictionary<String, Mediator.Row>();

        // Convert Generated class into Connector Row data.
        foreach (var command in rootObjects.PharosCs.CommandList.Command)
        {
            foreach (var row in command.Output.ResultSet.Rows)
            {
                var reconcileKey = row.findAdSalesReconcileKey();
                if (reconcileKey != null)
                {
                    reconcileToRow.Add(reconcileKey, row);
                }
            }
        }
        return reconcileToRow;
    }
    public static T XmlDeserializeFromString<T>(string xmlTextData)
    {
        using (TextReader reader = new StringReader(xmlTextData))
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }
    }

    public static T XmlDeserializeFromFile<T>(string path)
    {
        var text = ReadFile(path);
        return (T)XmlDeserializeFromString<T>(text);
    }

    public static T JsonDeserializeFromFile<T>(string path)
    {
        var text = ReadFile(path);
        return (T)JsonConvert.DeserializeObject<T>(text);
    }

    private static string ReadFile(string path)
    {
        try
        {
            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }
        catch (IOException ex)
        {
            throw new ApplicationException($"Error reading file {path}: {ex.Message}", ex);
        }
    }

    public static TValue GetValueOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue defaultValue)
    {
        return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public static DateTime? ISO8601(this Startdatetime startDateTime)
    {
        if (startDateTime == null)
            return null;
        if (startDateTime.GenericList == null || startDateTime.GenericList.Size != 1)
            return null;
        return startDateTime.GenericList.Object[0].ISO8601;
        
    }
}
