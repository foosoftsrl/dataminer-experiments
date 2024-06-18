namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Skyline.DataMiner.Scripting;
    public class AdSalesSource
    {
        public List<AdSalesRow> ReadAdSales(string channelName, string dir)
        {
            return ReadAdSales(channelName, dir, DateTime.Now);
        }
        public List<AdSalesRow> ReadAdSales(string channelName, string dir, DateTime firstDay)
        {
            var date = firstDay;
            var result = new List<AdSalesRow>();
            for (var i = -1; i < 3; i++)
            {
                string day = date.AddDays(i).ToString("yyyyMMdd");
                result.AddRange(ReadAdSales(channelName, dir, day));
            }
            return result;
        }
        public List<AdSalesRow> ReadAdSales(string channelName, string dir, string date)
        {
            string fileNamePrefix = $"{channelName}_{date}_";
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
