namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Utils;

    public static class WhatsonExtensions
    {
        public static List<WhatsonRow> Flatten(this Pharos whatsonData)
        {
            List<WhatsonRow> result = new List<WhatsonRow>();
            if (whatsonData.Playlist != null && whatsonData.Playlist.BlockList != null)
            {
                foreach (var blockList in whatsonData.Playlist.BlockList)
                {
                    if (blockList.PlaylistItem != null)
                    {
                        foreach (var playlistItem in blockList.PlaylistItem)
                        {
                            var reconcileKey = playlistItem.FindAdSalesReconcileKey();
                            var startTime = playlistItem.StartDateTime();
                            if (startTime != null)
                            {
                                result.Add(new WhatsonRow
                                {
                                    StartTime = (DateTime)startTime,
                                    ItemReference = playlistItem.ItemReference,
                                    ReconcileKey = reconcileKey,
                                    Title = playlistItem.ScheduledTitle,
                                    enablerLegacy = playlistItem.FindEnablerLegacyText(),
                                    scteBroadcastBreakStart = playlistItem.FindScteBroadcastBreakStartUpid(),
                                    scteBroadcastProviderAdvStart = playlistItem.FindScteBroadcastProviderAdvStartUpid(),
                                });
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static Dictionary<String, WhatsonRow> ToReconcileKeyMap(this List<WhatsonRow> whatsonRows)
        {
            Dictionary<String, WhatsonRow> reconcileToRow = new Dictionary<String, WhatsonRow>();
            foreach (var row in whatsonRows)
            {
                var adSalesReconcileKey = row.ReconcileKey;
                if (adSalesReconcileKey != null)
                {
                    reconcileToRow[adSalesReconcileKey] = row;
                }
            }
            return reconcileToRow;
        }
        public static string FindAdSalesReconcileKey(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            return playlistItem.FindDataElementByName("adSalesContentReconcileKey-text")?.Text();
        }

        public static PharosPlaylistBlockPlaylistItemTemplateDataElement FindScteBroadcastBreakStart(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            return playlistItem.FindDataElementByName("scteBroadcastBreakStart-insertSegmentationDescriptor");
        }

        public static string FindScteBroadcastBreakStartUpid(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            var item = playlistItem.FindScteBroadcastBreakStart();
            return item?.Value.DataElementCompoundList?.DataElementList.FindDataElementByName("segmentationUpid")?.Value;
        }

        public static PharosPlaylistBlockPlaylistItemTemplateDataElement FindScteBroadcastProviderAdvStart(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            return playlistItem.FindDataElementByName("scteBroadcastProviderAdvStart-insertSegmentationDescriptor");
        }

        public static string FindScteBroadcastProviderAdvStartUpid(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            var item = playlistItem.FindScteBroadcastProviderAdvStart();
            return item?.Value.DataElementCompoundList?.DataElementList.FindDataElementByName("segmentationUpid")?.Value;
        }

        public static PharosPlaylistBlockPlaylistItemTemplateDataElement FindEnablerLegacy(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            return playlistItem.FindDataElementByName("enablerLegacy-compoundList");
        }

        public static string FindEnablerLegacyText(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            var item = playlistItem.FindEnablerLegacy();
            return item?.Value.DataElementCompoundList?.DataElementList.FindDataElementByName("enablerLegacy-userText1")?.Value;
        }

        public static string Text(this PharosPlaylistBlockPlaylistItemTemplateDataElement dataElement)
        {
            if (dataElement.Value.Text != null && dataElement.Value.Text.Length == 1)
            {
                return dataElement.Value.Text[0];
            }
            return null;
        }

        public static PharosPlaylistBlockPlaylistItemTemplateDataElement FindDataElementByName(this PharosPlaylistBlockPlaylistItem playlistItem, String name)
        {
            return playlistItem.Template.DataElementList.FindDataElementByName(name);
        }

        public static PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundListDataElement FindDataElementByName(this PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundListDataElement[] dataElementArray, String name)
        {
            foreach (var entry in dataElementArray)
            {
                if (entry.Name == name)
                {
                    return entry;
                }
            }
            return null;
        }

        public static PharosPlaylistBlockPlaylistItemTemplateDataElement FindDataElementByName(this PharosPlaylistBlockPlaylistItemTemplateDataElement[] dataElementArray, String name)
        {
            foreach (var entry in dataElementArray)
            {
                if (entry.Name == name)
                {
                    return entry;
                }
            }
            return null;
        }

        public static DateTime? StartDateTime(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            if (playlistItem.StartDate == null || playlistItem.StartTimecode == null)
                return null;
            string date = playlistItem.StartDate.Substring(0, 10);
            string time = playlistItem.StartTimecode.Substring(0, 8);
            var dateTime = DateTime.Parse(date + "T" + time + "Z");
            return dateTime;

        }
    }
}
