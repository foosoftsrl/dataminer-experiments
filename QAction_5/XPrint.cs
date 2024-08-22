namespace QAction_5
{
    using System.Collections.Generic;
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
                List<AdSalesRow> adSalesRows = adSalesRowsGlobal.FindAll(row => row.DayOffset == day && row.Enabler != "P");
                List<WhatsonRow> whatsonRows = whatsonRowsGlobal.FindAll(row => row.DayOffset == day);
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
                                if (whatsonOrphanIndex.TryGetValue(reconcileKey, out var orphanWhatsonIdx))
                                {
                                    result.Add((adSalesRows[i], null, "warn_position_mismatch"));
                                    var entry = result[orphanWhatsonIdx];
                                    entry.Item3 = "warn_position_mismatch";
                                }
                                else
                                {
                                    result.Add((adSalesRows[i], null, "warn_only_adsales"));
                                    adsalesOrphanIndex.Add(reconcileKey, result.Count - 1);
                                }
                            }

                            for (var i = lastWhatsonIdx + 1; i < whatsonIdx; i++)
                            {
                                if (adsalesOrphanIndex.TryGetValue(reconcileKey, out var orphanAdsalesIdx))
                                {
                                    result.Add((null, whatsonRows[i], "warn_position_mismatch"));
                                    var entry = result[orphanAdsalesIdx];
                                    entry.Item3 = "warn_position_mismatch";
                                }
                                else
                                {
                                    result.Add((null, whatsonRows[i], "warn_only_whatson"));
                                    whatsonOrphanIndex.Add(reconcileKey, result.Count - 1);
                                }
                            }

                            var resultCode = "ok";
                            if(adSalesRows[adSalesIdx].ProductCode != whatsonRows[whatsonIdx].ProgramCode)
                            {
                                resultCode = "warn_material_mismatch";
                            }

                            result.Add((adSalesRows[adSalesIdx], whatsonRows[whatsonIdx], resultCode));
                            lastAdSalesIdx = adSalesIdx;
                            lastWhatsonIdx = whatsonIdx;
                        }
                    }
                }

                for (var i = lastAdSalesIdx + 1; i < adSalesRows.Count; i++)
                {
                    result.Add((adSalesRows[i], null, "warn_only_adsales"));
                }

                for (var i = lastWhatsonIdx + 1; i < whatsonRows.Count; i++)
                {
                    result.Add((null, whatsonRows[i], "warn_only_whatson"));
                }
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
