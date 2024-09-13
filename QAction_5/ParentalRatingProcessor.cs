namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ParentalRatingProcessor
    {
        public static List<ParentalRatingCheckEntry> Compute(List<WhatsonRow> whatsonData, List<MediatorRow> mediatorData, List<ParentalRatingRow> parentalRatingEvents, string channel, string mux)
        {
            var parentalRatingWhatsonData = whatsonData.FindAll(d => d.ParentalRatingValue != null).OrderBy(row => row.StartTime).ToList();
            var parentalRatingMediatorData = mediatorData.FindAll(d => d.ParentalRatingValue != null).OrderBy(row => row.StartTime).ToList();

            var mediatorMap = parentalRatingMediatorData.ToScheduleReferenceKeyMap();

            List<ParentalRatingCheckEntry> resultList = new List<ParentalRatingCheckEntry>();
            foreach (var whatsonRow in parentalRatingWhatsonData)
            {
                var timestamp = whatsonRow.StartTime;
                var mediatorRow = mediatorMap.GetValueOrDefault(whatsonRow.ItemReference, null);

                var message = "ok";

                var checkMediatorData = true;
                if (mediatorRow == null)
                {
                    message = "ko - No mediator data";
                    checkMediatorData = false;
                }
                else if (whatsonRow.ParentalRatingValue != mediatorRow.ParentalRatingValue)
                {
                    message = "ko - Wrong pr in mediator";
                    checkMediatorData = false;
                }

                ParentalRatingRow parentalRatingRow = null;
                if (mediatorRow != null)
                {
                    parentalRatingRow = ExtractParentalRatingFromProbe(parentalRatingEvents, timestamp, mediatorRow.ParentalRatingValue);
                }
                else
                {
                    parentalRatingRow = ExtractParentalRatingFromProbe(parentalRatingEvents, timestamp, whatsonRow.ParentalRatingValue);
                }

                var checkProbeData = true;
                long delta = 0;
                if (parentalRatingRow == null)
                {
                    message = "ko - No probe data";
                    checkProbeData = false;
                }
                else if(mediatorRow != null)
                {
                    delta = parentalRatingRow.TimeStamp.Ticks - mediatorRow.StartTime.Ticks;
                }
                else
                {
                    delta = parentalRatingRow.TimeStamp.Ticks - whatsonRow.StartTime.Ticks;
                }

                if (delta > 30000 || delta < -30000)
                {
                    message = "warn - high delta";
                    checkProbeData = false;
                }

                resultList.Add(new ParentalRatingCheckEntry
                {
                    Channel = channel,
                    Mux = mux,
                    ElementTime = timestamp,
                    WhatsonData = whatsonRow,
                    MediatorData = mediatorRow,
                    CheckMediatorData = checkMediatorData,
                    ParentalRatingRow = parentalRatingRow,
                    CheckProbeData = checkProbeData,
                    MuxTime = parentalRatingRow?.TimeStamp,
                    CheckResult = message,
                    Delta = delta,
                });
            }

            return resultList;
        }

        private static ParentalRatingRow ExtractParentalRatingFromProbe(List<ParentalRatingRow> parentalRatingEvents, DateTime timestamp, string parentalRatingValue)
        {
            for (int i = 0; i < parentalRatingEvents.Count; i++)
            {
                if (parentalRatingEvents[i].TimeStamp >= timestamp)
                {
                    if (parentalRatingValue == parentalRatingEvents[i].ParentalRating.ToString())
                    {
                        return parentalRatingEvents[i];
                    }
                    else if (i > 0 && parentalRatingValue == parentalRatingEvents[i - 1].ParentalRating.ToString())
                    {
                        return parentalRatingEvents[i - 1];
                    }
                }
            }

            return null;
        }
    }
}
