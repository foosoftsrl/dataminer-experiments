using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
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
        string channelName = (string)protocol.GetParameter(Parameter.channelnameadsales_7);
        string currentDate = DateTime.Now.ToString("yyyyMMdd");
        string dir = @"\\winfs01.mediaset.it\DM_Watchfolder\Adsales";
        string fileNamePrefix = $"{channelName}_{currentDate}_";
        string[] files = Directory.GetFiles(dir, $"{fileNamePrefix}*.xml");

        try
        {
            if (files.Length > 0)
            {
                protocol.Adsalesiterationcounter = (double)protocol.Adsalesiterationcounter + 1;
                string latestFile = files.OrderByDescending(f => File.GetLastWriteTime(f)).First();

                string fileContent = ReadFile(latestFile);
                var data = XmlDeserializeFromString<Data>(fileContent);

                // Convert Generated class into Connector Row data.
                var rows = ConvertToTableRows(data);
                protocol.FillArray(Parameter.Adsales.tablePid, rows, NotifyProtocol.SaveOption.Full);
                protocol.Adsalesdebugmsg = $"Parsed {data.Breaks.Length} breaks";
            }
            else
            {
                protocol.Adsalesdebugmsg = "File not found for the selected channel";
            }
        }
        catch (Exception ex)
        {
            protocol.Adsalesdebugmsg = "Failed parsing xml file";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    public static string ReadFile(string path)
    {
        using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
        {
            return streamReader.ReadToEnd();
        }
    }

    public static List<object[]> ConvertToTableRows(Data data)
    {
        List<object[]> rows = new List<object[]>();
        foreach (var break_ in data.Breaks)
        {
            foreach (var timeAllocation in break_.TimeAllocations)
            {
                foreach (var content in timeAllocation.Contents)
                {
                    rows.Add(new AdsalesQActionRow
                    {
                        Adsalesid = content.ContentReconcileKey,
                        Adsalestitle = content.ContentBrand,
                        Adsalestime = break_.BreakNominalTime,
                    }.ToObjectArray());
                }
            }
        }

        return rows;
    }

    public static T XmlDeserializeFromString<T>(this string xmlTextData)
    {
        using (TextReader reader = new StringReader(xmlTextData))
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }
    }

    public static string GenerateFileName(string channelName)
    {
        string currentDate = DateTime.Now.ToString("yyyyMMdd");
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        return $"{channelName}_{currentDate}_{timestamp}.xml";
    }
}
