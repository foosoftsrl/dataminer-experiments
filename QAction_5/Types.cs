#pragma warning disable SA1401 // C# does not like public fields

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
        public string TimeAllocationType; // STANDARD, PUSH, IS...
        public string Duration;
        public string Enabler;
        public int DayOffset;
    }

    public class WhatsonRow
    {
        public DateTime StartTime;
        public string ProgramCode;
        public string Title;
        public string ItemReference;
        public string ReconcileKey;
        public string TemplateName;
        public string ScteBroadcastBreakStart;
        public string ScteBroadcastProviderAdvStart;
        public string ScteBroadcastProviderOverlayPlacementStart;
        public string ScteBroadcastProviderOverlayPlacementEnd;
        public string EnablerLegacy;
        public int DayOffset;
    }

    public class MediatorRow
    {
        public int Id;
        public string ScheduleReference;
        public string ReconcileKey;
        public DateTime StartTime;
        public string Title;
        public string Status;
        public string ScteBroadcastBreakStart;
        public string ScteBroadcastProviderAdvStart;
        public string ScteBroadcastProviderOverlayPlacementStart;
        public string ScteBroadcastProviderOverlayPlacementEnd;
        public string EnablerLegacy;
        public string MaterialId;
        public int DayOffset;
    }

    public class EnablerRow
    {
        public DateTime TimeStamp;
        public int EventCode;
        public string EventName;
        public string Payload;
    }
}
