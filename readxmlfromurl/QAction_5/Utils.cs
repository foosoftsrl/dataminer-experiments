using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Mediator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QAction_5;
using Skyline.DataMiner.Scripting;

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
        public string Time;
        public PharosPlaylistBlock Block;
        public PharosPlaylistBlockPlaylistItem PlaylistItem;
        public string ReconcileKey;
    }

    public class MediatorRow
    {
        public string ScheduleReference;
        public string ReconcileKey;
        public string Title;
        public string Status;
        public DateTime StartTime;
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
        if (whatsonData.Playlist != null && whatsonData.Playlist.BlockList != null)
        {
            foreach (var blockList in whatsonData.Playlist.BlockList)
            {
                if (blockList.PlaylistItem != null) {
                    foreach (var playlistItem in blockList.PlaylistItem)
                    {
                        var reconcileKey = playlistItem.findAdSalesReconcileKey();
                        result.Add(new WhatsonRow
                        {
                            Time = playlistItem.startDateTime()?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                            Block = blockList,
                            PlaylistItem = playlistItem,
                            ReconcileKey = reconcileKey
                        });
                    }
                }
            }
        }
        return result;
    }

    public static Dictionary<String, WhatsonRow> toReconcileKeyMap(this List<WhatsonRow> whatsonRows)
    {
        Dictionary<String, WhatsonRow> reconcileToRow = new Dictionary<String, WhatsonRow>();
        foreach(var row in whatsonRows)
        {
            var adSalesReconcileKey = row.ReconcileKey;
            if (adSalesReconcileKey != null)
            {
                reconcileToRow.Add(adSalesReconcileKey, row);
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

    public static string getScheduleReference(this Mediator.Row row)
    {
        if (row.ScheduleReference == null)
            return null;
        if (row.ScheduleReference.GenericList == null)
            return null;
        if (row.ScheduleReference.GenericList.Size != 1)
            return null;
        return row.ScheduleReference.GenericList.Object[0];
    }

    public static List<MediatorRow> flatten(this Mediator.Rootobject rootObjects)
    {
        var result = new List<MediatorRow>();
        // Convert Generated class into Connector Row data.
        foreach (var command in rootObjects.PharosCs.CommandList.Command)
        {
            foreach (var row in command.Output.ResultSet.Rows)
            {
                var startTime = row.startDateTime();
                if (startTime == null)
                    continue;
                result.Add(new MediatorRow
                {
                    StartTime = (DateTime)startTime,
                    Title = row.Title.AsString(),
                    ReconcileKey = row.findAdSalesReconcileKey(),
                    ScheduleReference = row.getScheduleReference(),
                    Status = row.Status.GenericList.Object[0].TransferStatus,
                });
            }
        }
        return result;
    }

    public static Dictionary<string, Utils.MediatorRow> toReconcileKeyMap(this List<MediatorRow> mediatorRows)
    {
        var reconcileToRow = new Dictionary<String, Utils.MediatorRow>();

        // Convert Generated class into Connector Row data.
        foreach (var row in mediatorRows)
        {
            var reconcileKey = row.ReconcileKey;
            if (reconcileKey != null)
            {
                reconcileToRow.Add(reconcileKey, row);
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

    public static string ReadFile(string path)
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

    public static DateTime? startDateTime(this PharosPlaylistBlockPlaylistItem playlistItem)
    {
        if (playlistItem.StartDate == null || playlistItem.StartTimecode == null)
            return null;
        string date = playlistItem.StartDate.Substring(0, 10);
        string time = playlistItem.StartTimecode.Substring(0, 8);
        var dateTime = DateTime.Parse(date + "T" + time + "Z");
        return dateTime;
        
    }

    public static DateTime? startDateTime(this Mediator.Row row)
    {
        if (row.StartDateTime == null)
            return null;
        if (row.StartDateTime.GenericList == null || row.StartDateTime.GenericList.Size != 1)
            return null;
        return row.StartDateTime.GenericList.Object[0].ISO8601;
        
    }

    public static string channelName(this SLProtocolExt protocol)
    {
        var channelName = protocol.GetParameter(Parameter.channelname);
        if (!(channelName is string))
        {
            throw new Exception("Channel is not defined");
        }
        return (string)channelName;

    }

    public static string AsString(this Mediator.Title title)
    {
        if (title == null)
            return null;
        if (title.GenericList == null || title.GenericList.Size != 1)
            return null;
        return title.GenericList.Object[0];

    }
}
