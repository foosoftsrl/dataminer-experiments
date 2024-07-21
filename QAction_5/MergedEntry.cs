#pragma warning disable SA1401 // C# does not like public fields
#pragma warning disable SA1307 // field lower case
namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MergedEntry
    {
        public string channel;
        public string mux;
        public DateTime adSalesTime;
        public AdSalesRow adSalesData;
        public WhatsonRow whatsonData;
        public MediatorRow mediatorData;
        public EnablerRow scteBroadcastBreakStart;
        public EnablerRow scteBroadcastProviderAdvStart;
        public EnablerRow legacyEventLoad;
        public EnablerRow legacyEventStart;
        public EnablerRow legacyEventStop;
    }
}
