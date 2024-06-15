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
        public static MergedEntry[] Merge(List<AdSalesRow> adSalesData, List<WhatsonRow> whatsonData, List<MediatorRow> mediatorData)
        {
            var whatsonMap = whatsonData.toReconcileKeyMap();
            var mediatorMap = mediatorData.toReconcileKeyMap();
            List<MergedEntry> rowList = new List<MergedEntry>();
            foreach (var row in adSalesData)
            {
                var contentReconcileKey = row.ReconcileKey;
                rowList.Add(new MergedEntry
                {
                    adSalesTime = row.TimeOfDay,
                    adSalesData = row,
                    whatsonData = whatsonMap.GetValueOrDefault(contentReconcileKey, null),
                    mediatorData = mediatorMap.GetValueOrDefault(contentReconcileKey, null),
                });
            }
            return rowList.ToArray();
        }
    }
}
