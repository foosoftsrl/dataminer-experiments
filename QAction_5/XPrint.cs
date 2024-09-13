namespace QAction_5
{
    using System.Collections.Generic;
    using System.Linq;
    using Skyline.DataMiner.Net.Helper;

    public class XPrint
    {
        public static List<(AdSalesRow, WhatsonRow, string)> ComputeAdSalesWhatsonDiff(List<AdSalesRow> adSalesRowsGlobal, List<WhatsonRow> whatsonRowsGlobal)
        {
            /*
             * Result:
             * "ok" - ok
             * "warn_only_adsales" - no whatson entry - red
             * "warn_only_whatson" - no adsales entry - yellow
             * "warn_material_mismatch" - code mismatch
            */
            var result = new List<(AdSalesRow, WhatsonRow, string)>();
            for (var day = -1; day < 3; day++)
            {
                List<AdSalesRow> adSalesRows = adSalesRowsGlobal.FindAll(row => row.DayOffset == day);
                List<WhatsonRow> whatsonRows = whatsonRowsGlobal.FindAll(row => row.DayOffset == day);

                List<string> adSalesFilterOutReconcileKey = adSalesRows.FindAll(row => row.TimeAllocationType == "IS-BILLBOARD" || row.TimeAllocationType == "CIAK" || row.BreakScreenLayout == "OVL").Select(row => row.ReconcileKey).ToList();

                var reconcileKeyToWhatsonIndex = new Dictionary<string, int>();

                foreach (var (row, index) in whatsonRows.WithIndex())
                {
                    if (row.ReconcileKey != null)
                    {
                        reconcileKeyToWhatsonIndex.Add(row.ReconcileKey, index);
                    }
                }

                var whatsonOrphanIndex = new Dictionary<string, int>();
                var adsalesOrphanIndex = new Dictionary<string, int>();

                var standardDayResult = new List<(AdSalesRow, WhatsonRow, string)>();
                int lastAdSalesIdx = -1;
                int lastWhatsonIdx = -1;
                foreach (var (adSalesRow, adSalesIdx) in adSalesRows.WithIndex())
                {
                    var reconcileKey = adSalesRow.ReconcileKey;
                    if (adSalesFilterOutReconcileKey.Contains(reconcileKey))
                    {
                        lastAdSalesIdx++;
                        continue;
                    }

                    // First... find matching rows
                    if (reconcileKeyToWhatsonIndex.TryGetValue(reconcileKey, out var whatsonIdx))
                    {
                        if (whatsonIdx > lastWhatsonIdx)
                        {
                            for (var i = lastAdSalesIdx + 1; i < adSalesIdx; i++)
                            {
                                standardDayResult.Add((adSalesRows[i], null, "warn_only_adsales"));
                            }

                            for (var i = lastWhatsonIdx + 1; i < whatsonIdx; i++)
                            {
                                if(!adSalesFilterOutReconcileKey.Contains(whatsonRows[i].ReconcileKey))
                                {
                                    standardDayResult.Add((null, whatsonRows[i], "warn_only_whatson"));
                                }
                            }

                            var resultCode = "ok";
                            if(adSalesRows[adSalesIdx].ProductCode != whatsonRows[whatsonIdx].ProgramCode)
                            {
                                resultCode = "warn_material_mismatch";
                            }

                            standardDayResult.Add((adSalesRows[adSalesIdx], whatsonRows[whatsonIdx], resultCode));
                            lastAdSalesIdx = adSalesIdx;
                            lastWhatsonIdx = whatsonIdx;
                        }
                    }
                }

                for (var i = lastAdSalesIdx + 1; i < adSalesRows.Count; i++)
                {
                    standardDayResult.Add((adSalesRows[i], null, "warn_only_adsales"));
                }

                for (var i = lastWhatsonIdx + 1; i < whatsonRows.Count; i++)
                {
                    if (!adSalesFilterOutReconcileKey.Contains(whatsonRows[i].ReconcileKey))
                    {
                        standardDayResult.Add((null, whatsonRows[i], "warn_only_whatson"));
                    }
                }

                var billboardDayResult = new List<(AdSalesRow, WhatsonRow, string)>();
                List<AdSalesRow> adSalesBillboard = adSalesRows.FindAll(row => row.TimeAllocationType == "IS-BILLBOARD" || row.TimeAllocationType == "CIAK" || row.BreakScreenLayout == "OVL").ToList();
                foreach (var adSalesRow in adSalesBillboard)
                {
                    var reconcileKey = adSalesRow.ReconcileKey;
                    if (reconcileKeyToWhatsonIndex.TryGetValue(reconcileKey, out var whatsonIdx))
                    {
                        billboardDayResult.Add((adSalesRow, whatsonRows[whatsonIdx], "ok"));
                    }
                    else
                    {
                        billboardDayResult.Add((adSalesRow, null, "warn_only_adsales"));
                    }
                }

                var dayResult = new List<(AdSalesRow, WhatsonRow, string)>();
                var bilboardIndex = 0;
                foreach (var resultRow in standardDayResult)
                {
                    if (bilboardIndex < billboardDayResult.Count && resultRow.Item1 != null && billboardDayResult[bilboardIndex].Item1.TimeOfDay < resultRow.Item1.TimeOfDay)
                    {
                        dayResult.Add(billboardDayResult[bilboardIndex++]);
                    }

                    dayResult.Add(resultRow);
                }

                result.AddRange(dayResult);
            }

            return result;
        }

        public static List<(WhatsonRow, MediatorRow, string)> ComputeWhatsonMediatorDiff(List<WhatsonRow> whatsonRows, List<MediatorRow> mediatorRows)
        {
            /*
             * Result:
             * "ok" - ok
             * "warn_only_adsales" - no whatson entry - red
             * "warn_only_whatson" - no adsales entry - yellow
             * "warn_material_mismatch" - code mismatch
            */
            var result = new List<(WhatsonRow, MediatorRow, string)>();

            var reconcileKeyToWhatsonIndex = new Dictionary<string, int>();

            foreach (var (row, index) in whatsonRows.WithIndex())
            {
                if (row.ReconcileKey != null)
                {
                    reconcileKeyToWhatsonIndex[row.ReconcileKey] = index;
                }
            }

            int lastMediatorIdx = -1;
            int lastWhatsonIdx = -1;
            int currentDayOffset = -2;
            foreach (var (mediatorRow, mediatorIdx) in mediatorRows.WithIndex())
            {
                var reconcileKey = mediatorRow.ReconcileKey;

                // First... find matching rows
                if (reconcileKeyToWhatsonIndex.TryGetValue(reconcileKey, out var whatsonIdx))
                {
                    currentDayOffset = whatsonRows[whatsonIdx].DayOffset;
                    if (whatsonIdx > lastWhatsonIdx)
                    {
                        for (var i = lastMediatorIdx + 1; i < mediatorIdx; i++)
                        {
                            mediatorRows[i].DayOffset = currentDayOffset;
                            result.Add((null, mediatorRows[i], "warn_only_mediator"));
                        }

                        for (var i = lastWhatsonIdx + 1; i < whatsonIdx; i++)
                        {
                            result.Add((whatsonRows[i], null, "warn_only_whatson"));
                        }

                        var resultCode = "ok";
                        if (mediatorRows[mediatorIdx].MaterialId != whatsonRows[whatsonIdx].ProgramCode)
                        {
                            resultCode = "warn_material_mismatch";
                        }

                        mediatorRows[mediatorIdx].DayOffset = currentDayOffset;
                        result.Add((whatsonRows[whatsonIdx], mediatorRows[mediatorIdx], resultCode));
                        lastMediatorIdx = mediatorIdx;
                        lastWhatsonIdx = whatsonIdx;
                    }
                }
                else
                {
                    mediatorRows[mediatorIdx].DayOffset = currentDayOffset;
                }
            }

            for (var i = lastMediatorIdx + 1; i < mediatorRows.Count; i++)
            {
                mediatorRows[i].DayOffset = currentDayOffset;
                result.Add((null, mediatorRows[i], "warn_only_mediator"));
            }

            for (var i = lastWhatsonIdx + 1; i < whatsonRows.Count; i++)
            {
                result.Add((whatsonRows[i], null, "warn_only_whatson"));
            }

            return result;
        }
    }
}
