namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Skyline.DataMiner.Net.Upload;

    public class Palline
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
                if (adSalesRow.TimeAllocationType == "PUSH")
                {
                    // PUSH events may be scheduled in any event near the request...
                    // let's look for a matching one
                    whatsonRow = whatsonData.Find(s => s.enablerLegacy == adSalesRow.BreakId);
                    mediatorRow = mediatorData.Find(s => s.enablerLegacy == adSalesRow.BreakId);
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
                    channel = channel,
                    mux = mux,
                    adSalesTime = adSalesRow.TimeOfDay,
                    adSalesData = adSalesRow,
                    whatsonData = whatsonRow,
                    mediatorData = mediatorMap.GetValueOrDefault(contentReconcileKey, null),
                    scteBroadcastBreakStart = scteMap.GetValueOrDefault("SUPER_LOAD:" + whatsonRow?.scteBroadcastBreakStart, null),
                    scteBroadcastProviderAdvStart = scteMap.GetValueOrDefault("AD_START:" + whatsonRow?.scteBroadcastProviderAdvStart, null),
                    legacyEventLoad = legacyMap.GetValueOrDefault("LOAD:" + whatsonRow?.enablerLegacy, null),
                    legacyEventStart = legacyMap.GetValueOrDefault("START:" + whatsonRow?.enablerLegacy, null),
                    legacyEventStop = legacyMap.GetValueOrDefault("STOP:" + whatsonRow?.enablerLegacy, null),
                });
            }

            return rowList.ToArray();
        }
    }
}
