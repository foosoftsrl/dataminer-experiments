namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class DiffTool
    {
        public static List<(AdSalesRow, WhatsonRow)> ComputeDiff(List<AdSalesRow> adSalesRows, List<WhatsonRow> whatsonRows)
        {
            var result = new List<(AdSalesRow, WhatsonRow)>();
            var reconcileKeyToWhatsonIndex = new Dictionary<string, int>();
            // First... find matching rows
            foreach(var (row,index) in whatsonRows.WithIndex())
            {
                if (row.ReconcileKey != null)
                {
                    reconcileKeyToWhatsonIndex.Add(row.ReconcileKey, index);
                }
            }

            int lastAdSalesIdx = -1;
            int lastWhatsonIdx = -1;
            foreach(var (adSalesRow, adSalesIdx) in adSalesRows.WithIndex())
            {
                var reconcileKey = adSalesRow.ReconcileKey;
                if (reconcileKeyToWhatsonIndex.TryGetValue(reconcileKey, out var whatsonIdx))
                {
                    if (whatsonIdx > lastWhatsonIdx)
                    {
                        for (var i = lastAdSalesIdx + 1; i < adSalesIdx; i++)
                        {
                            result.Add((adSalesRows[i], null));
                        }
                        for (var i = lastWhatsonIdx + 1; i < whatsonIdx; i++)
                        {
                            result.Add((null, whatsonRows[i]));
                        }
                        result.Add((adSalesRows[adSalesIdx], whatsonRows[whatsonIdx]));
                        lastAdSalesIdx = adSalesIdx;
                        lastWhatsonIdx = whatsonIdx;
                    }
                }
            }
            for (var i = lastAdSalesIdx + 1; i < adSalesRows.Count; i++)
            {
                result.Add((adSalesRows[i], null));
            }
            for (var i = lastWhatsonIdx + 1; i < whatsonRows.Count; i++)
            {
                result.Add((null, whatsonRows[i]));
            }
            return result;
        }
    }
}
