namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Mediator;
    using Skyline.DataMiner.Scripting;

    public class EnablerSource
    {
        public async Task<List<EnablerRow>> ReadEnabler(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                };

                using (var response = await httpClient.SendAsync(request))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return parseText(apiResponse);
                }
            }
        }
        public static List<EnablerRow> parseText(String text)
        {
            List<EnablerRow> rows = new List<EnablerRow>();
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (trimmed.Length == 0)
                    continue;
                var cells = trimmed.Split(',');
                if (cells.Length < 4)
                {
                    throw new Exception("Invalid legacy row should contain at least 4 cells");
                }
                rows.Add(new EnablerRow
                {
                    TimeStamp = DateTime.Parse(cells[0]),
                    EventCode = int.Parse(cells[1]),
                    EventName = cells[2],
                    Payload = cells[3],
                });
            }
            return rows;
        }
    }
}
