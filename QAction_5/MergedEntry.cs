#pragma warning disable SA1401 // C# does not like public fields
namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MergedEntry
    {
        public string Channel;
        public string Mux;
        public DateTime AdSalesTime;
        public AdSalesRow AdSalesData;
        public WhatsonRow WhatsonData;
        public MediatorRow MediatorData;
        public EnablerRow ScteBroadcastBreakStart;
        public EnablerRow ScteBroadcastProviderAdvStart;
        public EnablerRow LegacyEventLoad;
        public EnablerRow LegacyEventStart;
        public EnablerRow LegacyEventStop;
    }
}
