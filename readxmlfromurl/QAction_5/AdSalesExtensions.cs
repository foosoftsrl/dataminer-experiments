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
                    var timeFromBreakStart = 0;
                    foreach (var content in timeAllocation.Contents)
                    {
                        var startTime = dayStart.AddSeconds(breakStart + timeFromBreakStart);
                        result.Add(new AdSalesRow
                        {
                            TimeOfDay = startTime,
                            Title = content.ContentBrand,
                            Type = timeAllocation.TimeAllocationType1,
                            ProductCode = content.ContentProductCode,
                            Duration = content.ContentTotalDuration,
                            ReconcileKey = content.ContentReconcileKey,
                        });
                        timeFromBreakStart += (content.ContentTotalDuration != null) ? Int32.Parse(content.ContentTotalDuration) : 0;
                    }
                }
            }
            return result;
        }
    }
}
