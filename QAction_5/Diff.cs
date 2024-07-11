namespace QAction_5
{
    using System.Collections.Generic;

    public class DiffTool
    {
        public static List<(AdSalesRow, WhatsonRow, int)> ComputeDiff(List<AdSalesRow> adSalesRowsGlobal, List<WhatsonRow> whatsonRowsGlobal)
        {
            /*
             * Result:
             * 0 - ok
             * 1 - no whatson entry - red
             * 2 - no adsales entry - yellow
             * 3 - code mismatch
            */
            var result = new List<(AdSalesRow, WhatsonRow, int)>();
            for (var day = -1; day < 3; day++)
            {
                List<AdSalesRow> adSalesRows = adSalesRowsGlobal.FindAll(row => row.DayOffset == day);
                List< WhatsonRow > whatsonRows = whatsonRowsGlobal.FindAll(row => row.DayOffset == day);
                var reconcileKeyToWhatsonIndex = new Dictionary<string, int>();

                foreach (var (row, index) in whatsonRows.WithIndex())
                {
                    if (row.ReconcileKey != null)
                    {
                        reconcileKeyToWhatsonIndex.Add(row.ReconcileKey, index);
                    }
                }

                int lastAdSalesIdx = -1;
                int lastWhatsonIdx = -1;
                foreach (var (adSalesRow, adSalesIdx) in adSalesRows.WithIndex())
                {
                    var reconcileKey = adSalesRow.ReconcileKey;

                    // First... find matching rows
                    if (reconcileKeyToWhatsonIndex.TryGetValue(reconcileKey, out var whatsonIdx))
                    {
                        if (whatsonIdx > lastWhatsonIdx)
                        {
                            for (var i = lastAdSalesIdx + 1; i < adSalesIdx; i++)
                            {
                                result.Add((adSalesRows[i], null, 1));
                            }

                            for (var i = lastWhatsonIdx + 1; i < whatsonIdx; i++)
                            {
                                result.Add((null, whatsonRows[i], 2));
                            }

                            var resultCode = 0;
                            if(adSalesRows[adSalesIdx].ProductCode != whatsonRows[whatsonIdx].ProgramCode)
                            {
                                resultCode = 3;
                            }

                            result.Add((adSalesRows[adSalesIdx], whatsonRows[whatsonIdx], resultCode));
                            lastAdSalesIdx = adSalesIdx;
                            lastWhatsonIdx = whatsonIdx;
                        }
                    }
                }

                for (var i = lastAdSalesIdx + 1; i < adSalesRows.Count; i++)
                {
                    result.Add((adSalesRows[i], null, 1));
                }

                for (var i = lastWhatsonIdx + 1; i < whatsonRows.Count; i++)
                {
                    result.Add((null, whatsonRows[i], 2));
                }
            }

            return result;
        }
    }
}
