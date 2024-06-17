
namespace QAction_5
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Skyline.DataMiner.Scripting;

    public class WhatsonSource
    {
        public List<WhatsonRow> ReadWhatson(string channelName, string dir)
        {
            return ReadWhatson(channelName, dir, DateTime.Now);
        }
        public List<WhatsonRow> ReadWhatson(string channelName, string dir, DateTime firstDay)
        {
            var date = firstDay;
            var result = new List<WhatsonRow>();
            for (var i = 0; i < 3; i++)
            {
                string day = date.ToString("yyyy-MM-dd");
                result.AddRange(ReadWhatson(channelName, dir, day));
                date = date.AddDays(1);
            }
            return result;
        }

        public List<WhatsonRow> ReadWhatson(string channelName, string dir, String date) 
        {   
            string[] files = Directory.GetFiles(dir, $"{channelName}_Schedule_{date}_*.xml");

            if (files.Length == 0)
            {
                return new List<WhatsonRow>();
            }
            string latestFile = files
                .OrderByDescending(f => GetFileVersion(f))
                .First();
            try
            {
                return Utils.XmlDeserializeFromFile<Pharos>(latestFile).Flatten();
            } catch(Exception ex)
            {
                throw new Exception($"Failed parsing Whatson file {latestFile}: {ex.Message}", ex);
            }
        }

        private int GetFileVersion(string filePath)
        {
            // Extract the file version using a regex that matches the 4 digit version number
            var match = Regex.Match(filePath, @"_(\d{4})_");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int version))
            {
                return version;
            }

            return 0; // default in case of no match
        }

    }
}
