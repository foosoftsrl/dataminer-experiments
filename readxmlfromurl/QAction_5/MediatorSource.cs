namespace QAction_5
{
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
    using Newtonsoft.Json;
    using Skyline.DataMiner.Scripting;

    internal class MediatorSource
    {
        public async Task<Mediator.Rootobject> ReadMediator(string uri, string channelName, int maxResults)
        {
            string sessionKey = "A-VDRFtKctjLhGR3wMmoITydeAeNjhME";
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

            return JsonConvert.DeserializeObject<Mediator.Rootobject>(jsonData);
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
}
