namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using static Utils;

    public static class WhatsonExtensions
    {
        public static List<WhatsonRow> Flatten(this Whatson.Pharos whatsonData)
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
                                    ProgramCode = playlistItem.FindProgramCode() ?? string.Empty,
                                    EnablerLegacy = playlistItem.FindEnablerLegacyText(),
                                    ScteBroadcastBreakStart = playlistItem.FindScteBroadcastBreakStartUpid(),
                                    ScteBroadcastProviderAdvStart = playlistItem.FindScteBroadcastProviderAdvStartUpid(),
                                    ScteBroadcastProviderOverlayPlacementStart = playlistItem.FindScteBroadcastProviderOverlayPlacementStartUpid(),
                                    ScteBroadcastProviderOverlayPlacementEnd = playlistItem.FindScteBroadcastProviderOverlayPlacementEndUpid(),
                                    TemplateName = playlistItem.Template.TemplateName,
                                    ParentalRatingValue = playlistItem.FindParentalRatingValue(),
                                });
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static List<WhatsonRow> FilterSpots(this List<WhatsonRow> list)
        {
            return list.FindAll(row => row.TemplateName == "SPOTAFF" || row.TemplateName == "SPOTNOAFF" || row.TemplateName == "CIAK");
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

        public static string FindAdSalesReconcileKey(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            return playlistItem.FindDataElementByName("adSalesContentReconcileKey-text")?.Text();
        }

        public static string FindProgramCode(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            var programCode = playlistItem.FindDataElementByName("materialSegment-matId")?.Text();
            if(programCode == null)
            {
                var item = playlistItem.FindDataElementByName("materialIncodeDuration-matIdIncodeDuration");
                programCode = item?.Value.DataElementCompoundList[0].FindDataElementByName("matId")?.Value.Text[0];
            }

            if (programCode == null)
            {
                programCode = playlistItem.FindDataElementByName("liveMatId-text")?.Text();
            }

            return programCode;
        }

        public static Whatson.DataElementType FindScteBroadcastBreakStart(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            return playlistItem.FindDataElementByName("scteBroadcastBreakStart-insertSegmentationDescriptor");
        }

        public static string FindScteBroadcastBreakStartUpid(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            var item = playlistItem.FindScteBroadcastBreakStart();
            return item?.Value.DataElementCompoundList[0]?.FindDataElementByName("segmentationUpid")?.Value.Text[0];
        }

        public static Whatson.DataElementType FindScteBroadcastProviderAdvStart(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            return playlistItem.FindDataElementByName("scteBroadcastProviderAdvStart-insertSegmentationDescriptor");
        }

        public static string FindScteBroadcastProviderAdvStartUpid(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            var item = playlistItem.FindScteBroadcastProviderAdvStart();
            return item?.Value.DataElementCompoundList[0]?.FindDataElementByName("segmentationUpid")?.Value.Text[0];
        }

        /* Enabler element parsing */
        public static string FindEnablerLegacyText(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            string result = null;
            var enablerLegacyCompountList = playlistItem.FindDataElementByName("enablerLegacy-compoundList");
            if (enablerLegacyCompountList?.Value.DataElementCompoundList != null)
            {
                foreach (var item in enablerLegacyCompountList.Value.DataElementCompoundList)
                {
                    foreach (var parameter in item)
                    {
                        if (parameter.Name == "enablerLegacy-userText1")
                        {
                            if (result == null)
                            {
                                result = parameter.Text() + string.Empty;
                            }
                            else
                            {
                                result += ";" + parameter.Text() + string.Empty;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /* SCTE Overlay Placement start */
        public static string FindScteBroadcastProviderOverlayPlacementStartUpid(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            string result = null;
            var sctePlacementStartCompountList = playlistItem.FindDataElementByName("scteBroadcastProviderOverlayPlacementStart-compoundList");
            if (sctePlacementStartCompountList?.Value.DataElementCompoundList != null)
            {
                foreach (var item in sctePlacementStartCompountList.Value.DataElementCompoundList)
                {
                    foreach (var parameter in item)
                    {
                        if (parameter.Name == "scteBroadcastProviderOverlayPlacementStart-insertSegmentationDescriptor")
                        {
                            var segmentationUpidParameter = parameter.Value.DataElementCompoundList[0]?.FindDataElementByName("segmentationUpid")?.Text();
                            if (result == null && segmentationUpidParameter != null)
                            {
                                result = segmentationUpidParameter + string.Empty;
                            }
                            else if (segmentationUpidParameter != null)
                            {
                                result += ";" + segmentationUpidParameter + string.Empty;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /* SCTE Overlay Placement end */
        public static string FindScteBroadcastProviderOverlayPlacementEndUpid(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            string result = null;
            var sctePlacementStartCompountList = playlistItem.FindDataElementByName("scteBroadcastProviderOverlayPlacementEnd-compoundList");
            if (sctePlacementStartCompountList?.Value.DataElementCompoundList != null)
            {
                foreach (var item in sctePlacementStartCompountList.Value.DataElementCompoundList)
                {
                    foreach (var parameter in item)
                    {
                        if (parameter.Name == "scteBroadcastProviderOverlayPlacementEnd-insertSegmentationDescriptor")
                        {
                            var segmentationUpidParameter = parameter.Value.DataElementCompoundList[0]?.FindDataElementByName("segmentationUpid")?.Text();
                            if (result == null && segmentationUpidParameter != null)
                            {
                                result = segmentationUpidParameter + string.Empty;
                            }
                            else if (segmentationUpidParameter != null)
                            {
                                result += ";" + segmentationUpidParameter + string.Empty;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static string Text(this Whatson.DataElementType dataElement)
        {
            if (dataElement.Value.Text != null && dataElement.Value.Text.Length == 1)
            {
                return dataElement.Value.Text[0];
            }

            return null;
        }

        public static Whatson.DataElementType FindDataElementByName(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem, String name)
        {
            return playlistItem.Template.DataElementList.FindDataElementByName(name);
        }

        public static Whatson.DataElementType FindDataElementByName(this Whatson.DataElementType[] dataElementArray, String name)
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

        public static DateTime? StartDateTime(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            if (playlistItem.StartDate == null || playlistItem.StartTimecode == null)
                return null;
            string date = playlistItem.StartDate.Substring(0, 10);
            string time = playlistItem.StartTimecode.Substring(0, 8);
            var dateTime = DateTime.Parse(date + "T" + time + "Z");
            return dateTime;
        }

        public static string FindParentalRatingValue(this Whatson.PharosPlaylistBlockPlaylistItem playlistItem)
        {
            var checkField = playlistItem.FindDataElementByName("parentalRating-graphic");
            if(checkField?.Value.Text.Length == 1 && checkField?.Value.Text[0] == "PR_ENGINE")
            {
                return playlistItem.FindDataElementByName("parentalRating-userText1")?.Value.Text[0];
            }

            return null;
        }
    }
}
