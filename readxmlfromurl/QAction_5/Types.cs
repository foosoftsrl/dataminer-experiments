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
        public string Title;
        public string ProductCode;
        public string ReconcileKey;
        public string Duration;
    }

    public class WhatsonRow
    {
        public DateTime StartTime;
        public string Title;
        public string ItemReference;
        public string ReconcileKey;
    }

    public class MediatorRow
    {
        public string ScheduleReference;
        public string ReconcileKey;
        public string Title;
        public string Status;
        public DateTime StartTime;
    }
}
