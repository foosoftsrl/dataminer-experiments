namespace QAction_5
{
    using System;
    using System.Collections.Generic;

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
                    whatsonRow = whatsonData.Find(s => s.EnablerLegacy?.Contains(adSalesRow.BreakId) ?? false);
                    mediatorRow = mediatorData.Find(s => s.EnablerLegacy?.Contains(adSalesRow.BreakId) ?? false);
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

                var scteBroadcastBreakStart = scteMap.GetValueOrDefault("SUPER_LOAD:" + mediatorRow?.ScteBroadcastBreakStart, null);
                var scteBroadcastProviderAdvStart = scteMap.GetValueOrDefault("AD_START:" + mediatorRow?.ScteBroadcastProviderAdvStart, null);
                var scteBroadcastProviderOverlayPlacementStart = scteMap.GetValueOrDefault("START:" + mediatorRow?.ScteBroadcastProviderOverlayPlacementStart, null);
                var scteBroadcastProviderOverlayPlacementEnd = scteMap.GetValueOrDefault("STOP:" + mediatorRow?.ScteBroadcastProviderOverlayPlacementEnd, null);

                var legacyEventLoad = legacyMap.GetValueOrDefault("LOAD:" + mediatorRow?.EnablerLegacy, null);
                var legacyEventStart = legacyMap.GetValueOrDefault("START:" + mediatorRow?.EnablerLegacy, null);
                var legacyEventStop = legacyMap.GetValueOrDefault("STOP:" + mediatorRow?.EnablerLegacy, null);

                int result = 0;
                string message = string.Empty;

                if(whatsonRow == null)
                {
                    result = 2;
                    message += "no data in whatson\n";
                }

                if (mediatorRow == null)
                {
                    result = 2;
                    message += "no data in mediator\n";
                }

                if (result == 0)
                {
                    if (adSalesRow.Enabler == "P" && mediatorRow.StartTime < DateTime.Now)
                    {
                        if(scteBroadcastProviderOverlayPlacementStart == null)
                        {
                            result = 2;
                            message += "missing scte overlay placement start\n";
                        }

                        if(legacyEventStart == null)
                        {
                            result = 2;
                            message += "missing legacy start\n";
                        }
                    }
                    else if (adSalesRow.Enabler == "E" && mediatorRow.StartTime < DateTime.Now)
                    {
                        if (scteBroadcastProviderOverlayPlacementStart == null)
                        {
                            result = 2;
                            message += "missing scte overlay placement start\n";
                        }

                        if (legacyEventStart == null)
                        {
                            result = 2;
                            message += "missing legacy start\n";
                        }
                    }
                    else if (adSalesRow.Enabler == "X" && mediatorRow.StartTime < DateTime.Now)
                    {
                        if (scteBroadcastBreakStart == null)
                        {
                            result = 2;
                            message += "missing scte adv start\n";
                        }

                        if (legacyEventStart == null)
                        {
                            result = 2;
                            message += "missing legacy start\n";
                        }
                    }
                }

                if (result == 0)
                {
                    message = "ok";
                    if(mediatorRow.StartTime > DateTime.Now)
                    {
                        result = 9;
                    }
                }

                var futureFilterFlag = 0;
                if(mediatorRow != null && mediatorRow.StartTime > DateTime.Now.AddMinutes(-5))
                {
                    futureFilterFlag = 1;
                }
                else if(mediatorRow == null && adSalesRow.TimeOfDay > DateTime.Now.AddMinutes(-5))
                {
                    futureFilterFlag = 1;
                }


                rowList.Add(new MergedEntry
                {
                    Channel = channel,
                    Mux = mux,
                    AdSalesTime = adSalesRow.TimeOfDay,
                    AdSalesData = adSalesRow,
                    WhatsonData = whatsonRow,
                    MediatorData = mediatorRow,
                    ScteBroadcastBreakStart = scteBroadcastBreakStart,
                    ScteBroadcastProviderAdvStart = scteBroadcastProviderAdvStart,
                    ScteBroadcastProviderOverlayPlacementStart = scteBroadcastProviderOverlayPlacementStart,
                    ScteBroadcastProviderOverlayPlacementEnd = scteBroadcastProviderOverlayPlacementEnd,
                    LegacyEventLoad = legacyEventLoad,
                    LegacyEventStart = legacyEventStart,
                    LegacyEventStop = legacyEventStop,
                    Result = result,
                    Message = message,
                    FutureFilterFlag = futureFilterFlag,
                });
            }

            return rowList.ToArray();
        }
    }
}
