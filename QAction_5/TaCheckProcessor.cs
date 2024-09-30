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

                int secondaryEventIndex = GetElementIndexFromStringArray(mediatorRow?.EnablerLegacy, adSalesRow.BreakId);

                var scteBroadcastBreakStart = scteMap.GetValueOrDefault("SUPER_LOAD:" + GetElementFromStringArray(mediatorRow?.ScteBroadcastBreakStart, secondaryEventIndex), null);
                var scteBroadcastProviderAdvStart = scteMap.GetValueOrDefault("AD_START:" + GetElementFromStringArray(mediatorRow?.ScteBroadcastProviderAdvStart, secondaryEventIndex), null);
                var scteBroadcastProviderOverlayPlacementStart = scteMap.GetValueOrDefault("START:" + GetElementFromStringArray(mediatorRow?.ScteBroadcastProviderOverlayPlacementStart, secondaryEventIndex), null);
                var scteBroadcastProviderOverlayPlacementEnd = scteMap.GetValueOrDefault("STOP:" + GetElementFromStringArray(mediatorRow?.ScteBroadcastProviderOverlayPlacementEnd, secondaryEventIndex), null);

                var legacyEventLoad = legacyMap.GetValueOrDefault("LOAD:" + GetElementFromStringArray(mediatorRow?.EnablerLegacy, secondaryEventIndex), null);
                var legacyEventStart = legacyMap.GetValueOrDefault("START:" + GetElementFromStringArray(mediatorRow?.EnablerLegacy, secondaryEventIndex), null);
                var legacyEventStop = legacyMap.GetValueOrDefault("STOP:" + GetElementFromStringArray(mediatorRow?.EnablerLegacy, secondaryEventIndex), null);

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

                DateTime? onairTime = null;
                if (result == 0)
                {
                    onairTime = mediatorRow.StartTime;
                    if(mediatorRow.EnablerLegacyOffset != null)
                    {
                        if (secondaryEventIndex >= 0)
                        {
                            onairTime = onairTime?.Add(DateTime.ParseExact(GetElementFromStringArray(mediatorRow.EnablerLegacyOffset, secondaryEventIndex), "HH:mm:ss:ff", null).TimeOfDay);
                        }
                    }

                    if (adSalesRow.Enabler == "P" && onairTime < DateTime.Now)
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
                    else if (adSalesRow.Enabler == "E" && onairTime < DateTime.Now)
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
                    else if (adSalesRow.Enabler == "X" && onairTime < DateTime.Now)
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
                    if(onairTime > DateTime.Now)
                    {
                        result = 9;
                    }
                }

                rowList.Add(new MergedEntry
                {
                    Channel = channel,
                    Mux = mux,
                    AdSalesTime = adSalesRow.TimeOfDay,
                    AdSalesData = adSalesRow,
                    WhatsonData = whatsonRow,
                    MediatorData = mediatorRow,
                    OnairTime = onairTime,
                    ScteBroadcastBreakStart = scteBroadcastBreakStart,
                    ScteBroadcastProviderAdvStart = scteBroadcastProviderAdvStart,
                    ScteBroadcastProviderOverlayPlacementStart = scteBroadcastProviderOverlayPlacementStart,
                    ScteBroadcastProviderOverlayPlacementEnd = scteBroadcastProviderOverlayPlacementEnd,
                    LegacyEventLoad = legacyEventLoad,
                    LegacyEventStart = legacyEventStart,
                    LegacyEventStop = legacyEventStop,
                    Result = result,
                    Message = message,
                });
            }

            return rowList.ToArray();
        }

        private static int GetElementIndexFromStringArray(string arrayString, string element)
        {
            if (arrayString == null || element == null)
            {
                return -1;
            }

            return Array.IndexOf(arrayString.Split(';'), element);
        }

        private static string GetElementFromStringArray(string arrayString, int index)
        {
            if (arrayString == null || index == -1)
            {
                return null;
            }

            string[] array = arrayString.Split(';');
            return array[index];
        }
    }
}
