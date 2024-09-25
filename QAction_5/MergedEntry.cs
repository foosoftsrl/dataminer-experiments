#pragma warning disable SA1401 // C# does not like public fields
namespace QAction_5
{
    using System;

    public class MergedEntry
    {
        public string Channel;
        public string Mux;
        public DateTime AdSalesTime;
        public AdSalesRow AdSalesData;
        public WhatsonRow WhatsonData;
        public MediatorRow MediatorData;
        public DateTime? OnairTime;
        public EnablerRow ScteBroadcastBreakStart;
        public EnablerRow ScteBroadcastProviderAdvStart;
        public EnablerRow ScteBroadcastProviderOverlayPlacementStart;
        public EnablerRow ScteBroadcastProviderOverlayPlacementEnd;
        public EnablerRow LegacyEventLoad;
        public EnablerRow LegacyEventStart;
        public EnablerRow LegacyEventStop;
        /*
         * 0 - Everything ok (green)
         * 1 - Warning (yellow)
         * 2 - Error (red)
         * 9 - Element ok but in the future (gray)
         */
        public int Result;
        public string Message;
    }
}
