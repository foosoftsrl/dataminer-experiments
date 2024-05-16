using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
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
        // string url = "https://test.foosoft.it/testDataminer.json";
        try
        {
            string url = (string)protocol.GetParameter(Parameter.urlmediator_9);
            List<object[]> instances = new List<object[]>();
            if (!IsValidUrl(url))
            {
                protocol.FillArray(Parameter.Mediator.tablePid, instances, NotifyProtocol.SaveOption.Full);
                protocol.Log("Invalid URL", LogType.Error, LogLevel.Level3);
                return;
            }

            protocol.Mediatoriterationcounter = (double)protocol.Mediatoriterationcounter + 1;
            string jsonData = ReadJsonFromUrl(url);

            Rootobject rootObjects = JsonConvert.DeserializeObject<Rootobject>(jsonData);

            // Convert Generated class into Connector Row data.
            foreach (var command in rootObjects.PharosCs.CommandList.Command)
            {
                foreach (var row in command.Output.ResultSet.Rows)
                {
                    instances.Add(new MediatorQActionRow
                    {
                        Mediatorid = row.TrimMaterialId.GenericList.Object[0],
                        Mediatordate = row.StartDateTime.GenericList.Object[0].ISO8601,
                    }.ToObjectArray());
                }
            }

            protocol.FillArray(Parameter.Mediator.tablePid, instances, NotifyProtocol.SaveOption.Full);
            protocol.Mediatordebugmsg = $"Processed {instances.Count} TrimMaterialId";

        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    public static string ReadJsonFromUrl(string url)
    {
        using (var client = new WebClient())
        {
            return client.DownloadString(url);
        }
    }

    public static bool IsValidUrl(string url)
    {
        try
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead(url))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
