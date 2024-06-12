using Skyline.DataMiner.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QAction_5
{
    public class WhatsonSource: IWhatsonSource
    {
        public Pharos ReadWhatson(string channelName, string dir)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string fileNamePattern = $"{channelName}_Schedule_{currentDate}_\\d{{4}}_*.xml";
            string[] files = Directory.GetFiles(dir, $"{channelName}_Schedule_{currentDate}_*.xml");

            if (files.Length > 0)
            {
                string latestFile = files
                    .OrderByDescending(f => GetFileVersion(f))
                    .First();
                return Utils.XmlDeserializeFromFile<Pharos>(latestFile);
            }
            else
            {
                throw new Exception("Could not find any Whatson file");
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
