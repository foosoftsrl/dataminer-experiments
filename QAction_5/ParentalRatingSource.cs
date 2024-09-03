using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QAction_5
{
    public class ParentalRatingSource
    {
        public static List<ParentalRatingRow> ParseText(String text)
        {
            List<ParentalRatingRow> rows = new List<ParentalRatingRow>();
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (trimmed.Length == 0)
                    continue;
                var cells = trimmed.Split(';');
                if (cells.Length == 0)
                {
                    throw new Exception("Invalid row should contain at least 4 cells");
                }

                rows.Add(new ParentalRatingRow
                {
                    TimeStamp = DateTime.Parse(cells[0]),
                    ParentalRating = cells[1] == string.Empty ? 0 : int.Parse(cells[1]),
                });
            }

            return rows;
        }

        public async Task<List<ParentalRatingRow>> ReadParentalRating(string url)
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
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception($"Invalid status code for URL {url}: {response.StatusCode}");
                    }

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return ParseText(apiResponse);
                }
            }
        }
    }
}
