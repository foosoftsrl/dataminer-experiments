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
                        var truncatedStartTime = startTime.Value.AddTicks(-(startTime.Value.Ticks % TimeSpan.TicksPerSecond));
                        result.Add(new MediatorRow
                        {
                            StartTime = (DateTime)truncatedStartTime,
                            Id = (int)row.Id.GenericList.Object[0],
                            Title = row.Title.AsString(),
                            ReconcileKey = row.FindAdSalesReconcileKey(),
                            ScheduleReference = row.GetScheduleReference(),
                            Status = row.Status.GenericList.Object[0].TransferStatus.ToString(),
                            EnablerLegacy = row.FindEnablerLegacyText(),
                            ScteBroadcastBreakStart = row.FindScteBroadcastBreakStartUpid(),
                            ScteBroadcastProviderAdvStart = row.FindScteBroadcastProviderAdvStartUpid(),
                            ScteBroadcastProviderOverlayPlacementStart = row.FindScteBroadcastProviderOverlayPlacementStartUpid(),
                            ScteBroadcastProviderOverlayPlacementEnd = row.FindScteBroadcastProviderOverlayPlacementEndUpid(),
                            MaterialId = row.GetTrimMaterialId(),
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

        public static Mediator.TemplateParameter FindScteBroadcastProviderOverlayPlacementStartCompoundList(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(TemplateParameterName.ScteBroadcastProviderOverlayPlacementStartCompoundList);
        }

        public static Mediator.TemplateParameter FindScteBroadcastProviderOverlayPlacementEndCompoundList(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(TemplateParameterName.ScteBroadcastProviderOverlayPlacementEndCompoundList);
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

        public static string FindScteBroadcastProviderOverlayPlacementStartUpid(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindScteBroadcastProviderOverlayPlacementStartCompoundList()?.Value.ValueClass?
                .TemplateParameterListCompound.FindTemplateParameterByName(TemplateParameterName.ScteBroadcastProviderOverlayPlacementStartInsertSegmentationDescriptor)?.Value.ValueClass?
                .TemplateParameterListCompound.GetValueByName(TemplateParameterName.SegmentationUpid);
        }

        public static string FindScteBroadcastProviderOverlayPlacementEndUpid(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindScteBroadcastProviderOverlayPlacementEndCompoundList()?.Value.ValueClass?
                .TemplateParameterListCompound.FindTemplateParameterByName(TemplateParameterName.ScteBroadcastProviderOverlayPlacementEndInsertSegmentationDescriptor)?.Value.ValueClass?
                .TemplateParameterListCompound.GetValueByName(TemplateParameterName.SegmentationUpid);
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
                        return parameter.Value.String + string.Empty;
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

        public static Mediator.TemplateParameter FindTemplateParameterByName(this Mediator.TemplateParameterListCompound mediatorRow, string name)
        {
            foreach (var entry in mediatorRow.TemplateParameterList)
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
    }
}
