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
                    reconcileToRow.Add(adSalesReconcileKey, row);
                }
            }
            return reconcileToRow;
        }
        public static string FindAdSalesReconcileKey(this PharosPlaylistBlockPlaylistItem playlistItem)
        {
            foreach (var entry in playlistItem.Template.DataElementList)
            {
                if (entry.Name == "adSalesContentReconcileKey-text")
                {
                    if (entry.Value.Text != null && entry.Value.Text.Length == 1)
                    {
                        return entry.Value.Text[0];
                    }
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
