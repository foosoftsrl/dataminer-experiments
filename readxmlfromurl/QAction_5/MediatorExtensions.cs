namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Utils;

    public static class MediatorExtensions
    {
        public static List<MediatorRow> Flatten(this Mediator.Rootobject rootObjects)
        {
            var result = new List<MediatorRow>();
            // Convert Generated class into Connector Row data.
            foreach (var command in rootObjects.PharosCs.CommandList.Command)
            {
                foreach (var row in command.Output.ResultSet.Rows)
                {
                    var startTime = row.StartDateTime();
                    if (startTime == null)
                        continue;
                    result.Add(new MediatorRow
                    {
                        StartTime = (DateTime)startTime,
                        Id = row.Id.GenericList.Object[0],
                        Title = row.Title.AsString(),
                        ReconcileKey = row.FindAdSalesReconcileKey(),
                        ScheduleReference = row.GetScheduleReference(),
                        Status = row.Status.GenericList.Object[0].TransferStatus,
                    });
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
        public static string FindAdSalesReconcileKey(this Mediator.Row mediatorRow)
        {
            foreach (var entry in mediatorRow.TemplateParameterList.GenericList.Object)
            {
                foreach (var templateParameter in entry.TemplateParameter)
                {
                    if (templateParameter.Name == "adSalesContentReconcileKey-text")
                    {
                        if (templateParameter.Value is string)
                        {
                            return (string)templateParameter.Value;
                        }
                    }
                }
            }
            return null;
        }

        public static Mediator.Templateparameter FindScteBroadcastBreakStart(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName("scteBroadcastBreakStart-insertSegmentationDescriptor");
        }

        public static Mediator.Templateparameter FindScteBroadcastProviderAdvStart(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName("scteBroadcastProviderAdvStart-insertSegmentationDescriptor");
        }

        public static Mediator.Templateparameter FindEnablerLegacy(this Mediator.Row mediatorRow)
        {
            return mediatorRow.FindTemplateParameterByName("enablerLegacy-compoundList");
        }

        public static Mediator.Templateparameter FindTemplateParameterByName(this Mediator.Row mediatorRow, string name)
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
            return DateTime.Parse(row.StartDateTime.GenericList.Object[0].ISO8601 + "Z");
        }
        public static string AsString(this Mediator.Title title)
        {
            if (title == null)
                return null;
            if (title.GenericList == null || title.GenericList.Size != 1)
                return null;
            return title.GenericList.Object[0];

        }
    }
}
