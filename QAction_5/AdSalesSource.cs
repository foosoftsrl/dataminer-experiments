namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class AdSalesSource
    {
        public List<AdSalesRow> ReadAdSales(string channelCode, string dir)
        {
            return ReadAdSales(channelCode, dir, DateTime.Now);
        }

        public List<AdSalesRow> ReadAdSales(string channelCode, string dir, DateTime firstDay)
        {
            var date = firstDay;
            var result = new List<AdSalesRow>();
            for (var i = -1; i < 3; i++)
            {
                string day = date.AddDays(i).ToString("yyyyMMdd");
                var partialResult = ReadAdSales(channelCode, dir, day);
                foreach(var row in partialResult)
                {
                    row.DayOffset = i;
                }

                result.AddRange(partialResult);
            }

            return result;
        }

        public List<AdSalesRow> ReadAdSales(string channelCode, string dir, string date)
        {
            string fileNamePrefix = $"{channelCode}_{date}_";
            string[] files = Directory.GetFiles(dir, $"{fileNamePrefix}*.xml");
            if (files.Length > 0)
            {
                string latestFile = files.OrderByDescending(f => File.GetLastWriteTime(f)).First();
                return Utils.XmlDeserializeFromFile<AdSales.DataType>(latestFile).Flatten();
            }

            return new List<AdSalesRow>();
        }
    }
}
