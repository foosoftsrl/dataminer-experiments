namespace QAction_5
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Mediator;
    using Skyline.DataMiner.Scripting;

    public static class Merger
    {
        public static MergedEntry[] Merge(List<AdSalesRow> adSalesData, List<WhatsonRow> whatsonData, List<MediatorRow> mediatorData, List<EnablerRow> scteEvents, List<EnablerRow> legacyEvents)
        {
            var whatsonMap = whatsonData.ToReconcileKeyMap();
            var mediatorMap = mediatorData.ToReconcileKeyMap();
            var scteMap = scteEvents.ToEventNamePayloadMap();
            var legacyMap = legacyEvents.ToEventNamePayloadMap(); // TODO: use event name + payload!!!
            List<MergedEntry> rowList = new List<MergedEntry>();
            foreach (var row in adSalesData)
            {
                var contentReconcileKey = row.ReconcileKey;
                var whatsonRow = whatsonMap.GetValueOrDefault(contentReconcileKey, null);
                rowList.Add(new MergedEntry
                {
                    adSalesTime = row.TimeOfDay,
                    adSalesData = row,
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
