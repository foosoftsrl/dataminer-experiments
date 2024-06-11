using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
    public static async Task Run(SLProtocolExt protocol)
    {
        var adSalesData = ReadAdSales(protocol);
        var mediatorData = await ReadMediator(protocol);
        var table = GenerateTable(adSalesData, mediatorData);
        protocol.FillArray(Parameter.Mergedtable.tablePid, table, NotifyProtocol.SaveOption.Full);
    }

    public static Data ReadAdSales(SLProtocolExt protocol)
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
                return XmlDeserializeFromString<Data>(fileContent);
            }
            else
            {
                protocol.Adsalesdebugmsg = "File not found for the selected channel";
                throw new Exception("Could not find any Adsales file");
            }
        }
        catch (FormatException fe)
        {
            protocol.Adsalesdebugmsg = "Date format error";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Date format exception thrown:{Environment.NewLine}{fe}", LogType.Error, LogLevel.NoLogging);
            throw fe;
        }
        catch (Exception ex)
        {
            protocol.Adsalesdebugmsg = "Failed parsing xml file";
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }

    public static string ReadFile(string path)
    {
        using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
        {
            return streamReader.ReadToEnd();
        }
    }

    public static List<object[]> GenerateTable(Data adSalesData, Dictionary<String, Row> mediatorData)
    {
        List<object[]> rows = new List<object[]>();
        foreach (var break_ in adSalesData.Breaks)
        {
            foreach (var timeAllocation in break_.TimeAllocations)
            {
                foreach (var content in timeAllocation.Contents)
                {
                    var contentReconcileKey = content.ContentReconcileKey.ToString();
                    Row mediatorRow = null;
                    if (mediatorData.ContainsKey(contentReconcileKey))
                    {
                        mediatorRow = mediatorData[contentReconcileKey.ToString()];
                    }

                    rows.Add(new MergedtableQActionRow
                    {
                        Mergedreconcilekey = content.ContentReconcileKey,
                        Mergedcontent = mediatorRow?.TrimMaterialId,
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

    public static async Task<Dictionary<String, Row>> ReadMediator(SLProtocolExt protocol)
    {
        List<object[]> instances = new List<object[]>();

        try
        {
            protocol.Mediatoriterationcounter = (double)protocol.Mediatoriterationcounter + 1;
            string uri = (string)protocol.GetParameter(Parameter.urimediator_9);
            string sessionKey = "A-VDRFtKctjLhGR3wMmoITydeAeNjhME";
            string channelName = (string)protocol.GetParameter(Parameter.channenamemediator_11);
            int maxResults = Convert.ToInt32(protocol.GetParameter(Parameter.maxresultsmediator_13));

            string cqlQuery = $"select parcel.templateparameterlist sequence.startdatetime sequence.id sequence.duration sequence.schedulereference parcel.title event.trimmaterialid event.infaderate event.intransitionname sequence.state status from '{channelName}' where maxresults = {maxResults} and event.stream in ('Main Video')";
            var payload = new
            {
                PharosCs = new
                {
                    CommandList = new
                    {
                        SessionKey = sessionKey,
                        Command = new[]
                        {
                            new
                            {
                                Subsystem = "playtime",
                                Method = "executeCQL",
                                ParameterList = new
                                {
                                    cql = new
                                    {
                                        String = cqlQuery,
                                    },
                                },
                            },
                        },
                    },
                },
            };
            string message = JsonConvert.SerializeObject(payload);

            string jsonData = await SendMessageAndWaitForResponseAsync(uri, message);

            var rootObjects = JsonConvert.DeserializeObject<Rootobject>(jsonData);

            Dictionary<String, Row> reconcileToRow = new Dictionary<String, Row>();

            // Convert Generated class into Connector Row data.
            foreach (var command in rootObjects.PharosCs.CommandList.Command)
            {
                foreach (var row in command.Output.ResultSet.Rows)
                {
                    if (row.TemplateParameterList.GenericList.Object[0].TemplateParameter[0].Name == "adSalesContentReconcileKey-text")
                    {
                        var reconcileKey = row.TemplateParameterList.GenericList.Object[0].TemplateParameter[0].Value;
                        reconcileToRow.Add((String)reconcileKey, row);
                    }
                }
            }

            protocol.FillArray(Parameter.Mediator.tablePid, instances, NotifyProtocol.SaveOption.Full);
            protocol.Mediatordebugmsg = $"Processed {instances.Count} Adsales Reconcile Key";
            return reconcileToRow;
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
            throw ex;
        }
    }

    public static async Task<string> SendMessageAndWaitForResponseAsync(string uri, string message)
    {
        if (!uri.StartsWith("ws://") && !uri.StartsWith("wss://"))
        {
            throw new ArgumentException("The URI must be a WebSocket URI starting with ws:// or wss://");
        }

        using (var clientWebSocket = new ClientWebSocket())
        {
            // Ignora gli errori di certificato SSL
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            try
            {
                Console.WriteLine($"Connecting to {uri}...");
                await clientWebSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                Console.WriteLine("Connected!");

                var bytesToSend = Encoding.UTF8.GetBytes(message);
                await clientWebSocket.SendAsync(new ArraySegment<byte>(bytesToSend), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine("Message sent!");

                string receivedMessage = string.Empty;
                bool isExpectedMessage = false;

                while (!isExpectedMessage)
                {
                    var buffer = new List<byte>();
                    var receiveBuffer = new ArraySegment<byte>(new byte[1024 * 4]);

                    WebSocketReceiveResult receiveResult;
                    do
                    {
                        receiveResult = await clientWebSocket.ReceiveAsync(receiveBuffer, CancellationToken.None);
                        buffer.AddRange(receiveBuffer.Array.Take(receiveResult.Count));
                    }
                    while (!receiveResult.EndOfMessage);

                    receivedMessage = Encoding.UTF8.GetString(buffer.ToArray());
                    Console.WriteLine($"Message received: {receivedMessage}");

                    // Check if the received message is the expected one
                    if (IsExpectedMessage(receivedMessage))
                    {
                        isExpectedMessage = true;
                    }
                }

                await clientWebSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                //await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
                Console.WriteLine("Connection closed.");

                return receivedMessage;
            }
            catch (WebSocketException wse)
            {
                Console.WriteLine($"WebSocket error: {wse.Message}");
                throw;
            }
            catch (WebException we)
            {
                Console.WriteLine($"Web error: {we.Message}");
                throw;
            }
            catch (IOException ioe)
            {
                Console.WriteLine($"IO error: {ioe.Message}");
                throw;
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Socket error: {se.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket error: {ex.Message}");
                throw;
            }
        }
    }

    private static bool IsExpectedMessage(string message)
    {
        // Implementa la logica per verificare se il messaggio ricevuto è quello desiderato.
        // Ad esempio, puoi verificare se il JSON contiene determinati campi o valori.
        return message.Contains("executeCQL");
    }
}
