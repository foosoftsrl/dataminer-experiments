namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using Mediator;
    using Skyline.DataMiner.Net.Helper;

    public static class MediatorExtensions
    {
        public static List<MediatorRow> Flatten(this Mediator.Welcome rootObject)
        {
            var result = new List<MediatorRow>();

            // Convert Generated class into Connector Row data.
            var commandList = rootObject?.PharosCs?.CommandList;
            if (commandList != null)
            {
                foreach (var command in commandList.Command)
                {
                    var rows = command?.Output?.ResultSet?.Rows;
                    if (rows == null)
                        continue;
                    foreach (var row in rows)
                    {
                        var startTime = row.StartDateTime();
                        if (startTime == null)
                            continue;
                        result.Add(new MediatorRow
                        {
                            StartTime = (DateTime)startTime,
                            Id = (int)row.Id.GenericList.Object[0],
                            Title = row.Title.AsString(),
                            ReconcileKey = row.FindAdSalesReconcileKey(),
                            ScheduleReference = row.GetScheduleReference(),
                            Status = row.Status.GenericList.Object[0].TransferStatus.ToString(),
                            enablerLegacy = row.FindEnablerLegacyText(),
                            scteBroadcastBreakStart = row.FindScteBroadcastBreakStartUpid(),
                            scteBroadcastProviderAdvStart = row.FindScteBroadcastProviderAdvStartUpid(),
                            materialId = row.GetTrimMaterialId(),
                        });
                    }
                }
            }

            return result;
        }

        public static List<MediatorRow> FilterSpots(this List<MediatorRow> list)
        {
            return list.FindAll(row => row.ReconcileKey != null);
        }

        public static Dictionary<string, MediatorRow> ToReconcileKeyMap(this List<MediatorRow> mediatorRows)
        {
            var reconcileToRow = new Dictionary<String, MediatorRow>();

            // Convert Generated class into Connector Row data.
            foreach (var row in mediatorRows)
            {
                var reconcileKey = row.ReconcileKey;
                if (reconcileKey != null)
                {
                    reconcileToRow[reconcileKey] = row;
                }
            }

            return reconcileToRow;
        }

        public static string FindAdSalesReconcileKey(this Mediator.Row row)
        {
            return row.FindTemplateParameterByName(TemplateParameterName.AdSalesContentReconcileKeyText)?.Value.String;
        }

        public static Mediator.TemplateParameter FindScteBroadcastBreakStart(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(TemplateParameterName.ScteBroadcastBreakStartInsertSegmentationDescriptor);
        }

        public static Mediator.TemplateParameter FindScteBroadcastProviderAdvStart(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(TemplateParameterName.ScteBroadcastProviderAdvStartInsertSegmentationDescriptor);
        }

        public static Mediator.TemplateParameter FindEnablerLegacy(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(TemplateParameterName.EnablerLegacyCompoundList);
        }

        public static string FindScteBroadcastBreakStartUpid(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindScteBroadcastBreakStart()?.Value.ValueClass?.TemplateParameterListCompound.GetValueByName(TemplateParameterName.SegmentationUpid);
        }

        public static string FindScteBroadcastProviderAdvStartUpid(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindScteBroadcastProviderAdvStart()?.Value.ValueClass?.TemplateParameterListCompound.GetValueByName(TemplateParameterName.SegmentationUpid);
        }

        public static string FindEnablerLegacyText(this Mediator.Row mediatorRow)
        {
            var enablerValue = mediatorRow.FindEnablerLegacy()?.Value.ValueClass;
            return enablerValue?.TemplateParameterListCompound.GetValueByName(TemplateParameterName.EnablerLegacyUserText1);
        }

        public static string GetValueByName(this TemplateParameterListCompound compound, string name)
        {
            foreach (var element in compound.TemplateParameterList)
            {
                foreach (var parameter in element.TemplateParameter)
                {
                    if (parameter.Name == name)
                    {
                        return parameter.Value.String + "";
                    }
                }
            }

            return null;
        }

        public static Mediator.TemplateParameter FindTemplateParameterByName(this Mediator.Row mediatorRow, string name)
        {
            foreach (var entry in mediatorRow.TemplateParameterList.GenericList.Object)
            {
                foreach (var templateParameter in entry.TemplateParameter)
                {
                    if (templateParameter.Name == name)
                    {
                        return templateParameter;
                    }
                }
            }

            return null;
        }

        public static string GetScheduleReference(this Mediator.Row row)
        {
            if (row.ScheduleReference == null)
                return null;
            if (row.ScheduleReference.GenericList == null)
                return null;
            if (row.ScheduleReference.GenericList.Size != 1)
                return null;
            return row.ScheduleReference.GenericList.Object[0];
        }

        public static string GetTrimMaterialId(this Mediator.Row row)
        {
            if (row.TrimMaterialId == null)
                return null;
            if (row.TrimMaterialId.GenericList == null)
                return null;
            if (row.TrimMaterialId.GenericList.Size != 1)
                return null;
            return row.TrimMaterialId.GenericList.Object[0];
        }

        public static DateTime? StartDateTime(this Mediator.Row row)
        {
            if (row.StartDateTime == null)
                return null;
            if (row.StartDateTime.GenericList == null || row.StartDateTime.GenericList.Size != 1)
                return null;
            return DateTime.Parse(row.StartDateTime.GenericList.Object[0].Iso8601 + "Z");
        }

        public static string AsString(this Mediator.InTransitionName title)
        {
            if (title == null)
                return null;
            if (title.GenericList == null || title.GenericList.Size != 1)
                return null;
            return title.GenericList.Object[0];
        }

        public static List<MediatorRow> ComputeDayOffsetFromWhatsonData(this List<MediatorRow> list, List<WhatsonRow> whatsonRowsGlobal)
        {
            bool insertDayOffset = false;
            for (var day = -1; day < 3; day++)
            {
                List<WhatsonRow> whatsonRows = whatsonRowsGlobal.FindAll(row => row.DayOffset == day);
                if (whatsonRows.IsNullOrEmpty())
                {
                    continue;
                }

                var reconcileKeyToWhatsonIndex = new Dictionary<string, int>();

                foreach (var (row, index) in whatsonRows.WithIndex())
                {
                    if (row.ReconcileKey != null)
                    {
                        reconcileKeyToWhatsonIndex.Add(row.ReconcileKey, index);
                    }
                }

                int lastMediatorIdx = -1;
                int lastWhatsonIdx = -1;
                foreach (var (mediatorRow, mediatorIdx) in list.WithIndex())
                {
                    var reconcileKey = mediatorRow.ReconcileKey;

                    if (reconcileKeyToWhatsonIndex.TryGetValue(reconcileKey, out var whatsonIdx))
                    {
                        if (whatsonIdx > lastWhatsonIdx)
                        {
                            for (var i = lastMediatorIdx + 1; i < mediatorIdx; i++)
                            {
                                list[i].DayOffset = whatsonRows[whatsonIdx].DayOffset;
                            }

                            list[mediatorIdx].DayOffset = whatsonRows[whatsonIdx].DayOffset;
                            lastMediatorIdx = mediatorIdx;
                            lastWhatsonIdx = whatsonIdx;
                            insertDayOffset = true;
                        }
                    }
                    else if(day == -1 || insertDayOffset)
                    {
                        list[mediatorIdx].DayOffset = whatsonRows[whatsonIdx].DayOffset;
                    }
                }

                if(day == 2)
                {
                    for (var i = lastMediatorIdx + 1; i < list.Count; i++)
                    {
                        list[i].DayOffset = whatsonRows[lastWhatsonIdx].DayOffset;
                    }
                }
            }

            return list;
        }
    }
}
