using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;

// using Newtonsoft.Json.Converters;
// using QAction_3.Json;

/// <summary>
/// The QAction entry point.
/// </summary>
/// <param name="protocol">Link with SLProtocol process.</param>
public static class QAction
{
    public static void Run(SLProtocolExt protocol)
    {
        string dir = "C:\\Skyline DataMiner\\ProtocolScripts";
        string fileName = "mediator.json";
        string fullPath = Path.Combine(dir, fileName);
        try
        {
            protocol.Jsoniterationcounter = (double)protocol.Jsoniterationcounter + 1;

            // Get Text based json data as a string.
            string source = ReadFile(fullPath);

            // Deserialize Json contents
            Rootobject rootObjects = JsonConvert.DeserializeObject<Rootobject>(source);
            List<object[]> instances = new List<object[]>();

            // Convert Generated class into Connector Row data.
            foreach (var command in rootObjects.PharosCs.CommandList.Command)
            {
                foreach (var row in command.Output.ResultSet.Rows) {
                    instances.Add(new DatatablejsonQActionRow
                    {
                        Datatablejsondataidcolumn = row.TrimMaterialId.GenericList.Object[0],
                        Datatablejsondatacolumnjson = row.StartDateTime.GenericList.Object[0].ISO8601,
                    }.ToObjectArray());
                }
            }

            protocol.FillArray(Parameter.Datatablejson.tablePid, instances, NotifyProtocol.SaveOption.Full);
            protocol.Jsondebugmsg = $"Processed {instances.Count} TrimMaterialId";
        }
        catch (Exception ex)
        {
            protocol.Jsondebugmsg = $"Failed parsing JSON file {ex.Message}";
            protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Deserializing JSON text failed due to:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
        }
    }

    public static string ReadFile(string path)
    {
        using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
        {
            return streamReader.ReadToEnd();
        }
    }
}
