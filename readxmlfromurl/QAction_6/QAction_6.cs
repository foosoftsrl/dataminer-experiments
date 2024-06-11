using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocolExt protocol)
    {
        string channelName = (string)protocol.GetParameter(Parameter.channelnamewon_15);
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        string dir = @"\\winfs01.mediaset.it\DM_Watchfolder\WON";
        string fileNamePattern = $"{channelName}_Schedule_{currentDate}_\\d{{4}}_*.xml";
        string[] files = Directory.GetFiles(dir, $"{channelName}_Schedule_{currentDate}_*.xml");

        //string fileNamePrefix = $"{channelName}_Schedule_{currentDate}_";
        // string[] files = Directory.GetFiles(dir, $"{fileNamePrefix}*.xml");

        try
        {
            if (files.Length > 0)
            {
                protocol.Woniterationcounter= (double)protocol.Woniterationcounter + 1;
                //string latestFile = files.OrderByDescending(f => File.GetLastWriteTime(f)).First();
                string latestFile = files
                    .OrderByDescending(f => GetFileVersion(f))
                    .First();

                string fileContent = ReadFile(latestFile);
                var data = XmlDeserializeFromString<Pharos>(fileContent);
                protocol.Wonfilename = latestFile;

                // Convert Generated class into Connector Row data.
                var rows = ConvertToTableRows(data);
                protocol.FillArray(Parameter.Won.tablePid, rows, NotifyProtocol.SaveOption.Full);
                protocol.Wondebugmsg= $"Parsed {data.Playlist.BlockList.Length} blocks";
            }
            else
            {
                protocol.Wondebugmsg = "File not found for the selected channel";
            }
        }
        catch (Exception ex)
        {
            protocol.Wondebugmsg = "Failed parsing xml file";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    public static string ReadFile(string path)
    {
        try
        {
            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }
        catch (IOException ex)
        {
            throw new ApplicationException($"Error reading file {path}: {ex.Message}", ex);
        }
    }

    public static List<object[]> ConvertToTableRows(Pharos data)
    {
        List<object[]> rows = new List<object[]>();
        foreach (var block_ in data.Playlist.BlockList)
        {
            foreach (var items in block_.PlaylistItem)
            {
                    rows.Add(new WonQActionRow
                    {
                        Wonschedulereference = items.ScheduleReference,
                        Wonstartdate= items.StartDate,
                    }.ToObjectArray());
            }
        }

        return rows;
    }

    public static T XmlDeserializeFromString<T>(this string xmlTextData)
    {
        try
        {
            using (TextReader reader = new StringReader(xmlTextData))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
        catch (InvalidOperationException ex)
        {
            throw new ApplicationException($"Error deserializing XML to {typeof(T).Name}: {ex.Message}", ex);
        }
    }

    private static int GetFileVersion(string filePath)
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
