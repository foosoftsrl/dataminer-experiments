namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AdSalesRow
    {
        public DateTime TimeOfDay;
        public string BreakId;
        public string ReconcileKey;
        public string Title;
        public string ProductCode;
        public string Type;
        public string Duration;
        public string Enabler;
    }

    public class WhatsonRow
    {
        public DateTime StartTime;
        public string Title;
        public string ItemReference;
        public string ReconcileKey;
        public bool scteBroadcastBreakStart;
        public bool scteBroadcastProviderAdvStart;
        public bool enablerLegacy;
    }

    public class MediatorRow
    {
        public int Id;
        public string ScheduleReference;
        public string ReconcileKey;
        public DateTime StartTime;
        public string Title;
        public string Status;
        public bool scteBroadcastBreakStart;
        public bool scteBroadcastProviderAdvStart;
        public bool enablerLegacy;
    }

    public class EnablerScteRow
    {
        public DateTime TimeStamp;
        public int EventCode;
        public string EventName;
        public string Payload;
    }
}
