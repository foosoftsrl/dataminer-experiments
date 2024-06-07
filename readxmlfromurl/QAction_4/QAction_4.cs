using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

            Rootobject rootObjects = JsonConvert.DeserializeObject<Rootobject>(jsonData);

            // Convert Generated class into Connector Row data.
            foreach (var command in rootObjects.PharosCs.CommandList.Command)
            {
                foreach (var row in command.Output.ResultSet.Rows)
                {
                    foreach (var parameter in row.TemplateParameterList.GenericList.Object[0].TemplateParameter)
                    {
                        if (parameter.Name == "adSalesContentReconcileKey-text")
                        {
                            instances.Add(new MediatorQActionRow
                            {
                                Mediatorid = row.Id.GenericList.Object[0],
                                Mediatorreconcilekey = row.TemplateParameterList.GenericList.Object[0].TemplateParameter[0].Value,
                                Mediatortitle = row.Title.GenericList.Object[0],
                                Mediatordate = row.StartDateTime.GenericList.Object[0].ISO8601,
                            }.ToObjectArray());
                        }
                    }
                }
            }

            protocol.FillArray(Parameter.Mediator.tablePid, instances, NotifyProtocol.SaveOption.Full);
            protocol.Mediatordebugmsg = $"Processed {instances.Count} Adsales Reconcile Key";
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
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
                await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
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
