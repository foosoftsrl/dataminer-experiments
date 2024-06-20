namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Mediator;
    using Skyline.DataMiner.Net.Upload;
    using static Utils;

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
                        });
                    }
                }
            }
            return result;
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
            return row.FindTemplateParameterByName(Mediator.PurpleName.AdSalesContentReconcileKeyText)?.Value.String;
        }

        public static Mediator.ObjectTemplateParameter FindScteBroadcastBreakStart(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(PurpleName.ScteBroadcastBreakStartInsertSegmentationDescriptor);
        }

        public static Mediator.ObjectTemplateParameter FindScteBroadcastProviderAdvStart(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(PurpleName.ScteBroadcastProviderAdvStartInsertSegmentationDescriptor);
        }

        public static Mediator.ObjectTemplateParameter FindEnablerLegacy(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName(Mediator.PurpleName.EnablerLegacyCompoundList);
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
                        return parameter.Value;
                    }

                }
            }
            return null;
        }

        public static Mediator.ObjectTemplateParameter FindTemplateParameterByName(this Mediator.Row mediatorRow, PurpleName name)
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
    }
}
