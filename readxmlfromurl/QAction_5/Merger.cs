using Mediator;
using Skyline.DataMiner.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAction_5
{

    public static class Merger
    {
        public static MergedEntry[] Merge(AdSales.DataType adSalesData, Pharos whatsonData, Mediator.Rootobject mediatorData)
        {
            var whatsonMap = whatsonData.toReconcileKeyMap();
            var mediatorMap = mediatorData.toReconcileKeyMap();
            List<MergedEntry> rowList = new List<MergedEntry>();
            foreach (var break_ in adSalesData.Breaks)
            {
                foreach (var timeAllocation in break_.TimeAllocations)
                {
                    foreach (var content in timeAllocation.Contents)
                    {
                        var contentReconcileKey = content.ContentReconcileKey;
                        rowList.Add(new MergedEntry
                        {
                            adSalesData = content,
                            whatsonData = whatsonMap.GetValueOrDefault(contentReconcileKey, null),
                            mediatorData = mediatorMap.GetValueOrDefault(contentReconcileKey, null),
                        });
                    }
                }
            }
            return rowList.ToArray();
        }
    }
}
