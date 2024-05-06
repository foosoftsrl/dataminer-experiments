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


    public static void Run(SLProtocolExt protocol)
    {
        try
        {
            protocol.Anumber = (double)protocol.Anumber + 2;
            // Get Text based xml data as a string.
            string textData = @"
                <items>
                  <item id=""1"" value=""just""/>
                  <item id=""2"" value=""some""/>
                  <item id=""3"" value=""random""/>
                  <item id=""4"" value=""data4""/>
                </items>
            ";
            // Convert C# Generated Classes into Qaction Rows.
            items items_ = XmlDeserializeFromString<items>(textData);

            // Convert Generated class into Connector Row data.
            var rows = ConvertToDamn(items_);
            protocol.Debug = items_.item.Length;
            protocol.FillArray(Parameter.Tablename.tablePid, rows, NotifyProtocol.SaveOption.Full);
        }
        catch (Exception ex)
        {
            protocol.Debug = "Failed parsing xml file"; ;
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
    public static List<object[]> ConvertToDamn(items items_)
    {
        List<object[]> instances = new List<object[]>();
        foreach (itemsItem instance in items_.item)
        {
            instances.Add(new TablenameQActionRow
            {
                Tablenameinstance_2001 = instance.id,
                Tablenameinstance2_2002 = instance.value,
            }.ToObjectArray());
        }
        return instances;
    }

    public static T XmlDeserializeFromString<T>(this string xmlTextData)
    {
        object result;
        try
        {
            using (TextReader reader = new StringReader(xmlTextData))
            {
                var serializer = new XmlSerializer(typeof(T));
                result = serializer.Deserialize(reader);
            }
            return (T)result;
        }
        catch
        {
            return default(T);
        }
    }
}
