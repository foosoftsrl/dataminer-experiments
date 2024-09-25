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

            ParentalRatingCheckEntry lastInsertedRow = null;
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

                var scheduledParentalRatingValue = mediatorRow?.ParentalRatingValue ?? whatsonRow.ParentalRatingValue;
                var scheduledParentalRatingStartTime = mediatorRow?.StartTime ?? whatsonRow.StartTime;

                ParentalRatingRow parentalRatingRow = null;
                long delta = 0;
                int checkProbeData = 0; // not checked
                if (timestamp <= DateTime.Now)
                {
                    checkProbeData = 1;
                    parentalRatingRow = ExtractParentalRatingFromProbe(parentalRatingEvents, timestamp, scheduledParentalRatingValue);
                    if (parentalRatingRow == null)
                    {
                        message = "ko - No probe data";
                        checkProbeData = -1;
                    }
                    else
                    {
                        delta = parentalRatingRow.TimeStamp.Ticks - scheduledParentalRatingStartTime.Ticks;
                    }

                    if ((lastInsertedRow == null || lastInsertedRow.ParentalRatingRow == null || parentalRatingRow == null ||
                        lastInsertedRow.ParentalRatingRow.ParentalRating != parentalRatingRow.ParentalRating) &&
                        (delta > 30000000 || delta < -30000000))
                    {
                        var deltaInSeconds = delta / 1000000;
                        message = "warn - high delta (" + deltaInSeconds + " s)";
                    }
                    else
                    {
                        // In this case maybe there is a problem with mediator, but on the mux everything is ok
                        message = "ok";
                    }
                }

                lastInsertedRow = new ParentalRatingCheckEntry
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
                };

                if (timestamp < DateTime.Today)
                {
                    // Filter out yesterday and before
                    continue;
                }

                if (timestamp > DateTime.Today.AddDays(2))
                {
                    // Filter out the day after tomorrow
                    continue;
                }

                resultList.Add(lastInsertedRow);
            }

            return resultList;
        }

        private static ParentalRatingRow ExtractParentalRatingFromProbe(List<ParentalRatingRow> parentalRatingEvents, DateTime scheduledTimestamp, string parentalRatingValue)
        {
            var currentPr = (parentalRatingEvents.First()?.ParentalRating ?? 0).ToString();
            for (int i = 0; i < parentalRatingEvents.Count; i++)
            {
                if (parentalRatingEvents[i].TimeStamp >= scheduledTimestamp)
                {
                    if (parentalRatingValue == currentPr && i > 0)
                    {
                        return parentalRatingEvents[i-1];
                    }
                    else if (parentalRatingValue == parentalRatingEvents[i].ParentalRating.ToString())
                    {
                        return parentalRatingEvents[i];
                    }
                }

                currentPr = parentalRatingEvents[i].ParentalRating.ToString();
            }

            if(currentPr == parentalRatingValue)
            {
                return parentalRatingEvents[parentalRatingEvents.Count - 1];
            }

            return null;
        }
    }
}
