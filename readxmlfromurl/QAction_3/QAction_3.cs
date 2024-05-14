using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
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
        string url = "https://test.foosoft.it/testDataminer.xml";
        try
        {
            protocol.Adsalesiterationcounter = (double)protocol.Adsalesiterationcounter + 1;
            string xmlData = ReadXmlFromUrl(url);
            var data = XmlDeserializeFromUrl<Data>(xmlData);

            var rows = ConvertToTableRows(data);
            protocol.FillArray(Parameter.Adsales.tablePid, rows, NotifyProtocol.SaveOption.Full);
            protocol.Adsalesdebugmsg = $"Parsed {data.Breaks.Length} breaks";
        }
        catch (Exception ex)
        {
            protocol.Adsalesdebugmsg = "Failed";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    public static string ReadXmlFromUrl(string url)
    {
        using (var client = new WebClient())
        {
            return client.DownloadString(url);
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

    private static T XmlDeserializeFromUrl<T>(string xmlData)
    {
        using (TextReader reader = new StringReader(xmlData))
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }
    }
}
