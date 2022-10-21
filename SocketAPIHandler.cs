using SocketIOClient;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Linq;

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
                var myDeserializedClass = JsonSerializer.Deserialize<Root>(eventData);
                string type = myDeserializedClass.type;
                string rawType = myDeserializedClass.type;
                string? donationAmount = myDeserializedClass.message[0].amount;

                if (rawType == "donation" && (MainWindow.invokeOnDonate ?? false))
                {
                    if (donationAmount != null && Int32.Parse(donationAmount) >= MainWindow.minimumDonationAmount)
                    {
                        waitForMessage((MainWindow.waitForMessageEndBool ?? false));
                    }
                }
                else if (rawType == "follow" && (MainWindow.invokeOnFollow ?? false))
                {
                    waitForMessage((MainWindow.waitForMessageEndBool ?? false));
                }
                else if ((rawType == "resub" || rawType == "subscription") && (MainWindow.invokeOnSubscription ?? false))
                {
                    waitForMessage((MainWindow.waitForMessageEndBool ?? false));
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
        }

        
        public class Message
        {
            public int id { get; set; }
            public string name { get; set; }
            public string amount { get; set; }
            public string formatted_amount { get; set; }
            public string formattedAmount { get; set; }
            public string message { get; set; }
            public string currency { get; set; }
            public object emotes { get; set; }
            public string iconClassName { get; set; }
            public To to { get; set; }
            public string from { get; set; }
            public object from_user_id { get; set; }
            public string _id { get; set; }
        }

        public class Root
        {
            public string type { get; set; }
            public List<Message> message { get; set; }
            public string event_id { get; set; }
        }

        public class To
        {
            public string name { get; set; }
        }
    }
}


