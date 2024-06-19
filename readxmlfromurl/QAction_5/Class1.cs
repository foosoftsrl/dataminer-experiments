namespace QAction_5
{
    using Skyline.DataMiner.Net.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    static class EnablerExtensions
    {
        public static Dictionary<String, EnablerRow> ToPayloadMap(this List<EnablerRow> rows)
        {
            var result = new Dictionary<String, EnablerRow>();

            // Convert Generated class into Connector Row data.
            foreach (var row in rows)
            {
                var payload = row.Payload;
                if (payload != null)
                {
                    result[payload] = row;
                }
            }
            return result;
        }
        public static Dictionary<String, EnablerRow> ToEventNamePayloadMap(this List<EnablerRow> rows)
        {
            var result = new Dictionary<String, EnablerRow>();

            // Convert Generated class into Connector Row data.
            foreach (var row in rows)
            {
                var payload = row.Payload;
                if (payload != null)
                {
                    result[row.EventName + ":" + payload] = row;
                }
            }
            return result;
        }

    }
}
