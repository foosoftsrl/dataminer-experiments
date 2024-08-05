namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Utils;

    public static class AdSalesExtensions
    {
        public static List<AdSalesRow> Flatten(this AdSales.DataType adSalesData)
        {
            List<AdSalesRow> result = new List<AdSalesRow>();
            var dayStart = DateTime.Parse(adSalesData.ScheduleDate);
            foreach (var break_ in adSalesData.Breaks)
            {
                var breakStart = TimeSpan.ParseExact(break_.BreakNominalTime, @"mm\:ss", null).TotalSeconds * 60;
                foreach (var timeAllocation in break_.TimeAllocations)
                {
                    if (timeAllocation.TimeAllocationType1 == "PUSH")
                    {
                        var startTime = dayStart.AddSeconds(breakStart);
                        result.Add(new AdSalesRow
                        {
                            TimeOfDay = startTime,
                            BreakId = break_.BreakID,
                            BreakPosition = "0",
                            Title = break_.BreakCommercialProductSales,
                            TimeAllocationType = timeAllocation.TimeAllocationType1,
                            ProductCode = string.Empty,
                            Duration = timeAllocation.TimeAllocationNominalDuration,
                            ReconcileKey = break_.BreakID,  // We are quite sure that break with timeallocationtype PUSH has single timeallocation
                            Enabler = "P",
                        });
                    }
                    else
                    {
                        var timeFromBreakStart = 0;
                        foreach (var content in timeAllocation.Contents)
                        {
                            var startTime = dayStart.AddSeconds(breakStart + timeFromBreakStart);
                            result.Add(new AdSalesRow
                            {
                                TimeOfDay = startTime,
                                BreakId = break_.BreakID,
                                BreakPosition = content.ContentOrder,
                                Title = content.ContentBrand,
                                TimeAllocationType = timeAllocation.TimeAllocationType1,
                                ProductCode = content.ContentProductCode,
                                Duration = content.ContentTotalDuration,
                                ReconcileKey = content.ContentReconcileKey,
                                Enabler = content.ContentEnabler,
                            });
                            timeFromBreakStart += (content.ContentTotalDuration != null) ? Int32.Parse(content.ContentTotalDuration) : 0;
                        }
                    }
                }
            }

            return result;
        }
    }
}
