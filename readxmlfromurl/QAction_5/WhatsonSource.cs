
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
        public Pharos ReadWhatson(string channelName, string dir)
        {   string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string fileNamePattern = $"{channelName}_Schedule_{currentDate}_\\d{{4}}_*.xml";
            string[] files = Directory.GetFiles(dir, $"{channelName}_Schedule_{currentDate}_*.xml");

            if (files.Length == 0)
            {
                return null;
            }
            string latestFile = files
                .OrderByDescending(f => GetFileVersion(f))
                .First();
            try
            {
                return Utils.XmlDeserializeFromFile<Pharos>(latestFile);
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
