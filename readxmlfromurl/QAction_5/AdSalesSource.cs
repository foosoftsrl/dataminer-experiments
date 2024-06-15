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
    internal class AdSalesSource
    {
        public AdSales.DataType ReadAdSales(string channelName, string dir)
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string fileNamePrefix = $"{channelName}_{date}_";
            string[] files = Directory.GetFiles(dir, $"{fileNamePrefix}*.xml");
            if (files.Length > 0)
            {
                string latestFile = files.OrderByDescending(f => File.GetLastWriteTime(f)).First();
                return Utils.XmlDeserializeFromFile<AdSales.DataType>(latestFile);
            }
            else
            {
                throw new Exception("Could not find any Adsales file");
            }
        }
    }

}
