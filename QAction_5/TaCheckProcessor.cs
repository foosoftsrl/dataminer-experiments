namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Skyline.DataMiner.Net.Upload;

    public class TaCheckProcessor
    {
        public static MergedEntry[] Compute(List<AdSalesRow> adSalesData, List<WhatsonRow> whatsonData, List<MediatorRow> mediatorData, List<EnablerRow> scteEvents, List<EnablerRow> legacyEvents, string channel, string mux)
        {
            var whatsonMap = whatsonData.ToReconcileKeyMap();
            var mediatorMap = mediatorData.ToReconcileKeyMap();
            var scteMap = scteEvents.ToEventNamePayloadMap();
            var legacyMap = legacyEvents.ToEventNamePayloadMap(); // TODO: use event name + payload!!!
            List<MergedEntry> rowList = new List<MergedEntry>();
            foreach (var adSalesRow in adSalesData)
            {
                var contentReconcileKey = adSalesRow.ReconcileKey;
                WhatsonRow whatsonRow;
                MediatorRow mediatorRow;
                if (adSalesRow.Enabler == "P")
                {
                    // PUSH events may be scheduled in any event near the request...
                    // let's look for a matching one
                    whatsonRow = whatsonData.Find(s => s.EnablerLegacy == adSalesRow.BreakId);
                    mediatorRow = mediatorData.Find(s => s.EnablerLegacy == adSalesRow.BreakId);
                }
                else if(adSalesRow.Enabler == "E" || adSalesRow.Enabler == "X")
                {
                    // Substitution / Enhancement events are scheduled for the ad itself
                    whatsonRow = whatsonMap.GetValueOrDefault(contentReconcileKey, null);
                    mediatorRow = mediatorMap.GetValueOrDefault(contentReconcileKey, null);
                }
                else
                {
                    continue;
                }

                rowList.Add(new MergedEntry
                {
                    Channel = channel,
                    Mux = mux,
                    AdSalesTime = adSalesRow.TimeOfDay,
                    AdSalesData = adSalesRow,
                    WhatsonData = whatsonRow,
                    MediatorData = mediatorRow,
                    ScteBroadcastBreakStart = scteMap.GetValueOrDefault("SUPER_LOAD:" + mediatorRow?.ScteBroadcastBreakStart, null),
                    ScteBroadcastProviderAdvStart = scteMap.GetValueOrDefault("AD_START:" + mediatorRow?.ScteBroadcastProviderAdvStart, null),
                    ScteBroadcastProviderOverlayPlacementStart = scteMap.GetValueOrDefault("START:" + mediatorRow?.ScteBroadcastProviderOverlayPlacementStart, null),
                    ScteBroadcastProviderOverlayPlacementEnd = scteMap.GetValueOrDefault("STOP:" + mediatorRow?.ScteBroadcastProviderOverlayPlacementEnd, null),
                    LegacyEventLoad = legacyMap.GetValueOrDefault("LOAD:" + mediatorRow?.EnablerLegacy, null),
                    LegacyEventStart = legacyMap.GetValueOrDefault("START:" + mediatorRow?.EnablerLegacy, null),
                    LegacyEventStop = legacyMap.GetValueOrDefault("STOP:" + mediatorRow?.EnablerLegacy, null),
                });
            }

            return rowList.ToArray();
        }
    }
}
