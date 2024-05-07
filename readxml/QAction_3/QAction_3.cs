using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

    public static string readFile(string path)
    {
        using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
        {
            return streamReader.ReadToEnd();
        }
    }

    public static void Run(SLProtocolExt protocol)
    {
        string dir = "C:\\Skyline DataMiner\\ProtocolScripts";
        string fileName = "sample.xml";
        string fullPath = Path.Combine(dir, fileName);
        try
        {
            string fileContent = readFile(fullPath);
            protocol.Anumber = (double)protocol.Anumber + 1;

            var data = XmlDeserializeFromString<items>(fileContent);

            // Convert Generated class into Connector Row data.
            var rows = ConvertToTableRows(data);
            protocol.Debug = data.item.Length;
            protocol.FillArray(Parameter.Tablename.tablePid, rows, NotifyProtocol.SaveOption.Full);
        }
        catch (Exception ex)
        {
            protocol.Debug = "Failed parsing xml file";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    public static List<object[]> ConvertToTableRows(items items_)
    {
        List<object[]> rows = new List<object[]>();
        foreach (itemsItem instance in items_.item)
        {
            rows.Add(new TablenameQActionRow
            {
                Tablenameinstance_2001 = instance.id,
                Tablenameinstance2_2002 = instance.value,
            }.ToObjectArray());
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
}
