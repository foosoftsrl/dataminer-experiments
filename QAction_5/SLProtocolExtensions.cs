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
        public static string ChannelName(this SLProtocolExt protocol)
        {
            var channelName = protocol.GetParameter(Parameter.channelname);
            if (!(channelName is string))
            {
                throw new Exception("Channel is not defined");
            }

            return (string)channelName;
        }

        public static string MuxName(this SLProtocolExt protocol)
        {
            var channelName = protocol.GetParameter(Parameter.muxname);
            if (!(channelName is string))
            {
                throw new Exception("Mux is not defined");
            }

            return (string)channelName;
        }

        public static void PublishAlarmBoxData(this SLProtocolExt protocol, List<AdSalesRow> adSalesData, List<WhatsonRow> whatsonData, List<MediatorRow> mediatorData, List<(AdSalesRow, WhatsonRow, string)> adsalesWonDiff, List<(WhatsonRow, MediatorRow, string)> wonMediatorDiff)
        {
            for (var i = 0; i < 3; i++)
            {
                var from = DateTime.Today.AddDays(i);

                var adSalesDayData = adSalesData.FindAll(row => row.DayOffset == i);
                var whatsonDayData = whatsonData.FindAll(row => row.DayOffset == i);
                var mediatorDayData = mediatorData.FindAll(row => row.DayOffset == i);

                var adsalesWonDiffDay = adsalesWonDiff.FindAll(row =>
                {
                    if (row.Item1 != null)
                    {
                        return row.Item1.DayOffset == i;
                    }
                    else
                    {
                        return row.Item2.DayOffset == i;
                    }
                });
                var wonMediatorDiffDay = wonMediatorDiff.FindAll(row =>
                {
                    if (row.Item1 != null)
                    {
                        return row.Item1.DayOffset == i;
                    }
                    else
                    {
                        return row.Item2.DayOffset == i;
                    }
                });

                var adSalesCount = adSalesDayData.Count();
                var whatsonCount = whatsonDayData.Count();
                var mediatorCount = mediatorDayData.Count();

                var adsalesWonErrorFlag = adSalesCount == whatsonCount && adsalesWonDiffDay.FindAll(item => item.Item3 != "ok").IsNullOrEmpty();
                var wonMediatorErrorFlag = whatsonCount == mediatorCount && wonMediatorDiffDay.FindAll(item => item.Item3 != "ok").IsNullOrEmpty();

                switch (i)
                {
                    case 0:
                        protocol.Alarmboxdate0 = from.ToString("yyyy-MM-dd");
                        protocol.Alarmboxadsalesitems0 = adSalesCount;
                        protocol.Alarmboxwonitems0 = whatsonCount;
                        protocol.Alarmboxmediatoritems0 = mediatorCount;
                        protocol.Alarmboxadsaleswonalarm0 = adsalesWonErrorFlag ? 0 : 1;
                        protocol.Alarmboxwonmediatoralarm0 = wonMediatorErrorFlag ? 0 : 1;
                        break;
                    case 1:
                        protocol.Alarmboxdate1 = from.ToString("yyyy-MM-dd");
                        protocol.Alarmboxadsalesitems1 = adSalesCount;
                        protocol.Alarmboxwonitems1 = whatsonCount;
                        protocol.Alarmboxmediatoritems1 = mediatorCount;
                        protocol.Alarmboxadsaleswonalarm1 = adsalesWonErrorFlag ? 0 : 1;
                        protocol.Alarmboxwonmediatoralarm1 = wonMediatorErrorFlag ? 0 : 1;
                        break;
                    case 2:
                        protocol.Alarmboxdate2 = from.ToString("yyyy-MM-dd");
                        protocol.Alarmboxadsalesitems2 = adSalesCount;
                        protocol.Alarmboxwonitems2 = whatsonCount;
                        protocol.Alarmboxmediatoritems2 = mediatorCount;
                        protocol.Alarmboxadsaleswonalarm2 = adsalesWonErrorFlag ? 0 : 1;
                        protocol.Alarmboxwonmediatoralarm2 = wonMediatorErrorFlag ? 0 : 1;
                        break;
                }
            }
        }

        public static void PublishAdSalesWhatsonDiffTable(this SLProtocolExt protocol, List<(AdSalesRow, WhatsonRow, string)> rows)
        {
            var tableRows = new List<object[]>();
            var idx = 0;
            foreach (var row in rows)
            {
                tableRows.Add(new AdsaleswondiffQActionRow
                {
                    Adsaleswondiffkey = (idx++).ToString(),
                    Adsaleswondiffadsalesreconcilekey = row.Item1?.ReconcileKey ?? string.Empty,
                    Adsaleswondiffadsalesstarttime = row.Item1?.TimeOfDay.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Adsaleswondiffadsalesprogramcode = row.Item1?.ProductCode ?? string.Empty,
                    Adsaleswondiffadsalesprogramtitle = row.Item1?.Title ?? string.Empty,
                    Adsaleswondiffwonreconcilekey = row.Item2?.ReconcileKey ?? string.Empty,
                    Adsaleswondiffwonstarttime = row.Item2?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Adsaleswondiffwonprogramcode = row.Item2?.ProgramCode ?? string.Empty,
                    Adsaleswondiffwonprogramtitle = row.Item2?.Title ?? string.Empty,
                    Adsaleswondiffdayoffset = row.Item1 != null ? row.Item1?.DayOffset.ToString() : row.Item2?.DayOffset.ToString(),
                    Adsaleswondiffresult = row.Item3,
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Adsaleswondiff.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        }

        public static void PublishMediatorWonDiffTable(this SLProtocolExt protocol, List<(WhatsonRow, MediatorRow, string)> rows)
        {
            var tableRows = new List<object[]>();
            var idx = 0;
            foreach (var row in rows)
            {
                tableRows.Add(new WonmediatordiffQActionRow
                {
                    Wonmediatordiffkey = (idx++).ToString(),
                    Wonmediatordiffwonreconcilekey = row.Item1?.ReconcileKey ?? string.Empty,
                    Wonmediatordiffwonstarttime = row.Item1?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Wonmediatordiffwonprogramcode = row.Item1?.ProgramCode ?? string.Empty,
                    Wonmediatordiffwonprogramtitle = row.Item1?.Title ?? string.Empty,
                    Wonmediatordiffmediatorreconcilekey = row.Item2?.ReconcileKey ?? string.Empty,
                    Wonmediatordiffmediatorstarttime = row.Item2?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Wonmediatordiffmediatorprogramcode = row.Item2?.MaterialId ?? string.Empty,
                    Wonmediatordiffmediatorprogramtitle = row.Item2?.Title ?? string.Empty,
                    Wonmediatordiffdayoffset = row.Item1 != null ? row.Item1?.DayOffset.ToString() : row.Item2?.DayOffset.ToString(),
                    Wonmediatordiffresult = row.Item3,
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Wonmediatordiff.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
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
                    Adsalestype = row.TimeAllocationType,
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
                    Wonenablerlegacy = row.EnablerLegacy ?? string.Empty,
                    Wonsctebreakstart = row.ScteBroadcastBreakStart ?? string.Empty,
                    Wonscteadvstart = row.ScteBroadcastProviderAdvStart ?? string.Empty,
                    Wontemplatename = row.TemplateName ?? string.Empty,
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
                    Mediatorenablerlegacy = row.EnablerLegacy ?? string.Empty,
                    Mediatorsctebreakstart = row.ScteBroadcastBreakStart ?? string.Empty,
                    Mediatorscteadvstart = row.ScteBroadcastProviderAdvStart ?? string.Empty,
                    Mediatormaterialid = row.MaterialId ?? string.Empty,
                    Mediatordayoffset = row.DayOffset,
                    Mediatorsctebroadcastprovideroverlayplacementstart = row.ScteBroadcastProviderOverlayPlacementStart ?? string.Empty,
                    Mediatorsctebroadcastprovideroverlayplacementend = row.ScteBroadcastProviderOverlayPlacementEnd ?? string.Empty,
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
                string enablerProbe = string.Empty;
                if(row.LegacyEventLoad != null || row.LegacyEventStart != null || row.LegacyEventStop != null)
                {
                    enablerProbe += (row.LegacyEventLoad != null) ? "L" : "-";
                    enablerProbe += (row.LegacyEventStart != null) ? "P" : "-";
                    enablerProbe += (row.LegacyEventStop != null) ? "S" : "-";
                }

                string scteProbe = string.Empty;
                if (row.ScteBroadcastBreakStart != null || row.ScteBroadcastProviderAdvStart != null)
                {
                    scteProbe += (row.ScteBroadcastBreakStart != null) ? "L" : "-";
                    scteProbe += (row.ScteBroadcastProviderAdvStart != null) ? "P" : "-";
                }

                if(row.ScteBroadcastProviderOverlayPlacementStart != null || row.ScteBroadcastProviderOverlayPlacementEnd != null)
                {
                    scteProbe += (row.ScteBroadcastProviderOverlayPlacementStart != null) ? "P" : "-";
                    scteProbe += (row.ScteBroadcastProviderOverlayPlacementEnd != null) ? "S" : "-";
                }

                tableRows.Add(new TachecktableQActionRow
                {
                    Tacheckreconcilekey = row.AdSalesData.ReconcileKey,
                    Tacheckproductcode = row.AdSalesData.ProductCode,
                    Tacheckchannel = row.Channel,
                    Tacheckmux = row.Mux,
                    Tacheckadsalestime = row.AdSalesTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Tacheckhavewon = (row.WhatsonData != null) ? "\u2713" : string.Empty,
                    Tacheckhavemediator = (row.MediatorData != null) ? "✓" : string.Empty,
                    Tacheckmediatortime = row.MediatorData?.StartTime.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                    Tachecktype = row.AdSalesData.Enabler,
                    Tacheckenablerprobe = enablerProbe,
                    Tacheckscteprobe = scteProbe,
                    Tacheckresult = row.Result,
                    Tacheckmessage = row.Message,
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Tachecktable.tablePid, tableRows, NotifyProtocol.SaveOption.Full);
        }

        public static string GetParameterDescriptionAsString(this SLProtocolExt protocol, int parameterId)
        {
            var description = protocol.GetParameterDescription(parameterId);
            if(description is string)
            {
                return (string)description;
            }
            else
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
                }
                catch(Exception)
                {
                    throw new Exception($"Invalid value '{value}' for parameter {protocol.GetParameterDescription(parameterId)}, should be an int");
                }
            }
            else if(value == null)
            {
                throw new Exception($"Missing parameter {protocol.GetParameterDescription(parameterId)}");
            }
            else
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
