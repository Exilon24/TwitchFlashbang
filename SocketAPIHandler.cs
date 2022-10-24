using SocketIOClient;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace TwitchFlashbang
{
    public class SocketAPIHandler
    {
        public static SocketIO sio;
        public static async Task startConnection()
        {

            // Create the socket object.
            var token = MainWindow.socketAPIToken;
            sio = new SocketIO($"https://sockets.streamlabs.com?token={token}", new SocketIOOptions()
            {
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
                Query = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("token", token )
                },
                EIO = 3
            });

            sio.OnConnected += async (s, e) =>
            {
                MessageBox.Show("Successfully connected to streamlabs.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            };



            sio.On("event", data =>
            {
                JsonDocument eventData = data.GetValue<JsonDocument>();
                var parsedData = ToJsonString(eventData);
                var OvverideType = eventData.RootElement.GetProperty("type");
                string type = String.Empty;
                int? donationAmount = 0;
                Root myDeserializedClass = new Root();

                // Dear god
                if (OvverideType.ToString() == "donation" || OvverideType.ToString() == "follow" || OvverideType.ToString() == "resub" || OvverideType.ToString() == "subscription")
                {
                    Trace.WriteLine(parsedData);
                    myDeserializedClass = JsonSerializer.Deserialize<Root>(parsedData);
                    donationAmount = myDeserializedClass.message[0].amount;
                    type = myDeserializedClass.type;


                    if (type == "donation" && (MainWindow.invokeOnDonate ?? false))
                    {
                        if (donationAmount != null && donationAmount >= MainWindow.minimumDonationAmount)
                        {
                            waitForMessage((MainWindow.waitForMessageEndBool ?? false));
                        }
                    }
                    else if (type == "follow" && (MainWindow.invokeOnFollow ?? false))
                    {
                        waitForMessage((MainWindow.waitForMessageEndBool ?? false));
                    }
                    else if ((type == "resub" || type == "subscription") && (MainWindow.invokeOnSubscription ?? false))
                    {
                        waitForMessage((MainWindow.waitForMessageEndBool ?? false));
                    }
                }
            });

            await sio.ConnectAsync();

            async Task waitForMessage(bool shouldWait)
            {
                if (shouldWait)
                {
                    await Task.Delay(7000);
                    MainWindow.gameOverlay.CSGOflash();
                }
                else
                {
                    MainWindow.gameOverlay.CSGOflash();
                }

            }

            static string ToJsonString(JsonDocument jdoc)
            {
                using (var stream = new MemoryStream())
                {
                    Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                    jdoc.WriteTo(writer);
                    writer.Flush();
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
        }

        
        public class Message
        {
            public int id { get; set; } = 0;
            public string? name { get; set; } = string.Empty;
            public int? amount { get; set; } = 0;
            public string? formatted_amount { get; set; } = string.Empty;
            public string? formattedAmount { get; set; } = string.Empty;
            public string? message { get; set; } = string.Empty;
            public string? currency { get; set; } = string.Empty;
            public object emotes { get; set; } = string.Empty;
            public string? iconClassName { get; set; } = string.Empty;
            public To? to { get; set; }
            public string? from { get; set; } = string.Empty;
            public object from_user_id { get; set; } = string.Empty;
            public string? _id { get; set; } = string.Empty;
            public string? created_at { get; set; } = string.Empty;
        }

        public class Root
        {
            public string? type { get; set; } = string.Empty;
            public List<Message>? message { get; set; }
            public string? event_id { get; set; } = string.Empty;
            public string? @for { get; set; } = string.Empty;
        }

        public class To
        {
            public string? name { get; set; } = string.Empty;
        }
    }
}


