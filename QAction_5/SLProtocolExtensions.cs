namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using Mediator;
    using Skyline.DataMiner.Net.Helper;
    using Skyline.DataMiner.Net.SLSearch.Messages;
    using Skyline.DataMiner.Scripting;

    public static class SLProtocolExtensions
    {
        public static string channelName(this SLProtocolExt protocol)
        {
            var channelName = protocol.GetParameter(Parameter.channelname);
            if (!(channelName is string))
            {
                throw new Exception("Channel is not defined");
            }
            return (string)channelName;
        }

        public static void PublishXPrintTable(this SLProtocolExt protocol, List<AdSalesRow> adSalesData, List<WhatsonRow> whatsonRows, List<MediatorRow> mediatorRows) 
        {
            List<object[]> tableRows = new List<object[]>();
            for (var i = 0; i < 3; i++)
            {
                var from = DateTime.Today.AddDays(i);
                var to = DateTime.Today.AddDays(i + 1);
                var adSalesCount = adSalesData.Count(row => row.TimeOfDay >= from && row.TimeOfDay < to);
                var whatsonCount = whatsonRows.Count(row => row.StartTime >= from && row.StartTime < to);
                var mediatorCount = mediatorRows.Count(row => row.StartTime >= from && row.StartTime < to);
                var text = adSalesCount + "/" + whatsonCount + "/" + mediatorCount;
                var errorCount = new Random().Next(2);
                tableRows.Add(new XprintQActionRow
                {
                    Xprintindex = i,
                    Xprintdate = from.ToString("yyyy-MM-dd HH:mm:ss"),
                    Xprintadsales = adSalesCount,
                    Xprintwhatson = whatsonCount,
                    Xprintmediator = mediatorCount,
                    Xprinterrors = errorCount,
                }.ToObjectArray());

                switch (i)
                {
                    case 0:
                        protocol.Xprintdate0 = from.ToString("yyyy-MM-dd");
                        protocol.Xprintdata0 = text;
                        protocol.Xprintalarm0 = errorCount;
                        break;
                    case 1:
                        protocol.Xprintdate1 = from.ToString("yyyy-MM-dd");
                        protocol.Xprintdata1 = text;
                        protocol.Xprintalarm1 = errorCount;
                        break;
                    case 2:
                        protocol.Xprintdate2 = from.ToString("yyyy-MM-dd");
                        protocol.Xprintdata2 = text;
                        protocol.Xprintalarm2 = errorCount;
                        break;
                }
            }
            protocol.FillArray(Parameter.Xprint.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        }

        public static void PublishXPrintDiffTable(this SLProtocolExt protocol, List<(AdSalesRow, WhatsonRow)> rows)
        {
            var tableRows = new List<object[]>();
            var idx = 0;
            foreach (var row in rows)
            {
                tableRows.Add(new XprintdiffQActionRow
                {
                    Xprintdiffkey = "ciao" + idx++,
                    Xprintdiffleftreconcilekey = row.Item1?.ReconcileKey ?? "",
                    Xprintdiffleftstarttime = row.Item1?.TimeOfDay.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                    Xprintdiffleftprogramcode = row.Item1?.ProductCode ?? "",
                    Xprintdiffleftprogramtitle = row.Item1?.Title,
                    Xprintdiffrightreconcilekey = row.Item2?.ReconcileKey ?? "",
                    Xprintdiffrightstarttime = row.Item2?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                    Xprintdiffrightprogramcode = row.Item2?.ReconcileKey ?? "",
                    Xprintdiffrightprogramtitle = row.Item2?.Title,
                }.ToObjectArray());
            }
            protocol.FillArray(Parameter.Xprintdiff.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        }

        public static void PublishEnablerLegacyTable(this SLProtocolExt protocol, List<EnablerRow> rows)
        {
            var tableRows = new List<object[]>();
            foreach (var row in rows)
            {
                tableRows.Add(new EnablerlegacyQActionRow
                {
                    Enablerlegacytime = row.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    Enablerlegacyeventcode = row.EventCode.ToString(),
                    Enablerlegacyeventname = row.EventName.ToString(),
                    Enablerlegacypayload = row.Payload.ToString(),
                }.ToObjectArray());
            }
            protocol.FillArray(Parameter.Enablerlegacy.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        }

        public static void PublishScteTable(this SLProtocolExt protocol, List<EnablerRow> rows)
        {
            var tableRows = new List<object[]>();
            foreach (var row in rows)
            {
                tableRows.Add(new EnablerscteQActionRow
                {
                    Enablersctetime = row.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    Enablerscteeventcode = row.EventCode.ToString(),
                    Enablerscteeventname = row.EventName.ToString(),
                    Enablersctepayload = row.Payload.ToString(),
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Enablerscte.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        }

        public static List<object[]> PublishAdsalesTable(this SLProtocolExt protocol, List<AdSalesRow> adSalesRows)
        {
            List<object[]> tableRows = new List<object[]>();
            foreach (var row in adSalesRows)
            {
                tableRows.Add(new AdsalesQActionRow
                {
                    Adsalestime = row.TimeOfDay,
                    Adsalesbreakid = row.BreakId,
                    Adsalesreconcilekey = row.ReconcileKey,
                    Adsalestitle = row.Title,
                    Adsalestype = row.Type,
                    Adsalesenabler = row.Enabler,
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Adsales.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
            return tableRows;
        }

        public static List<object[]> PublishWhatsonTable(this SLProtocolExt protocol, List<WhatsonRow> whatsonRows)
        {
            List<object[]> tableRows = new List<object[]>();
            foreach (var row in whatsonRows)
            {
                tableRows.Add(new WonQActionRow
                {
                    Wonstartdate = row.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Wontitle = row.Title,
                    Wonreconcilekey = row.ReconcileKey ?? string.Empty,
                    Wonitemreference = row.ItemReference,
                    Wonenablerlegacy = row.enablerLegacy ?? string.Empty,
                    Wonsctebreakstart = row.scteBroadcastBreakStart ?? string.Empty,
                    Wonscteadvstart = row.scteBroadcastProviderAdvStart ?? string.Empty,
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Won.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
            return tableRows;
        }

        public static List<object[]> PublishMediatorTable(this SLProtocolExt protocol, List<MediatorRow> mediatorRows)
        {
            List<object[]> tableRows = new List<object[]>();
            foreach (var row in mediatorRows)
            {
                tableRows.Add(new MediatorQActionRow
                {
                    Mediatorid = row.Id,
                    Mediatorschedulereference = row.ScheduleReference ?? string.Empty,
                    Mediatordate = row.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Mediatortitle = row.Title,
                    Mediatorstatus = row.Status,
                    Mediatorreconcilekey = row.ReconcileKey ?? string.Empty,
                    Mediatorenablerlegacy = row.enablerLegacy ?? string.Empty,
                    Mediatorsctebreakstart = row.scteBroadcastBreakStart ?? string.Empty,
                    Mediatorscteadvstart = row.scteBroadcastProviderAdvStart ?? string.Empty,
                }.ToObjectArray());
            }
            protocol.FillArray(Parameter.Mediator.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
            return tableRows;
        }
        public static void PublishMergedTable(this SLProtocolExt protocol, MergedEntry[] mergedRows)
        {
            List<object[]> tableRows = new List<object[]>();
            foreach (var row in mergedRows)
            {
                tableRows.Add(new MergedtableQActionRow
                {
                    Mergedreconcilekey = row.adSalesData.ReconcileKey,
                    Mergedproductcode = row.adSalesData.ProductCode,
                    Mergedduration = row.adSalesData.Duration,
                    Mergedadsalestime = row.adSalesTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Mergedhavewon = (row.whatsonData != null) ? "\u2713" : string.Empty,
                    Mergedhavemediator = (row.mediatorData != null) ? "✓" : string.Empty,
                    Mergedwontime = row.whatsonData?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Mergedmediatortime = row.mediatorData?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Mergedtype = row.adSalesData.Type,
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Mergedtable.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        }

        public static string GetParameterDescriptionAsString(this SLProtocolExt protocol, int parameterId)
        {
            var description = protocol.GetParameterDescription(parameterId);
            if(description is string)
            {
                return (string)description;
            } else
            {
                return "???";
            }
        }
        public static int GetRequiredIntParameter(this SLProtocolExt protocol, int parameterId)
        {
            var value = protocol.GetParameter(parameterId);
            if(value is string)
            {
                try
                {
                    return Convert.ToInt32(value);
                } catch(Exception ex)
                {
                    throw new Exception($"Invalid value '{value}' for parameter {protocol.GetParameterDescription(parameterId)}, should be an int");
                }
            } else if(value == null) {
                throw new Exception($"Missing parameter {protocol.GetParameterDescription(parameterId)}");
            } else
            {
                throw new Exception($"Unexpected type for parameter {protocol.GetParameterDescription(parameterId)}");
            }
        }

        public static string GetRequiredNonEmptyStringParameter(this SLProtocolExt protocol, int parameterId)
        {
            var value = protocol.GetParameter(parameterId);
            if (value is string)
            {
                var valueString = (string)value;
                if(valueString.Trim().IsNullOrEmpty())
                {
                    throw new Exception($"Invalid value '{value}' for parameter {protocol.GetParameterDescription(parameterId)}, should be a non empty string");
                }
                return valueString;
            }
            else if (value == null)
            {
                throw new Exception($"Missing parameter {protocol.GetParameterDescription(parameterId)}");
            }
            else
            {
                throw new Exception($"Unexpected type for parameter {protocol.GetParameterDescription(parameterId)}");
            }
        }
    }
}
