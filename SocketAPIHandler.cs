using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using SocketIOClient;

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
                JsonElement root = eventData.RootElement;
                JsonElement type = root.GetProperty("type");
                string rawType = type.ToString();
                if (rawType == "donation" && (MainWindow.invokeOnDonate ?? false))
                {
                    MainWindow.gameOverlay.CSGOflash();
                }
                else if (rawType == "follow" && (MainWindow.invokeOnFollow ?? false))
                {
                    MainWindow.gameOverlay.CSGOflash();
                }
                else if ((rawType == "resub" || rawType == "subscription") && (MainWindow.invokeOnSubscription ?? false))
                {
                    MainWindow.gameOverlay.CSGOflash();
                }
            });

            await sio.ConnectAsync();
        }
    }
    public class Message
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? amount { get; set; }
        public string? formatted_amount { get; set; }
        public string? formattedAmount { get; set; }
        public string? message { get; set; }
        public string? currency { get; set; }
        public object? emotes { get; set; }
        public string? iconClassName { get; set; }
        public To? to { get; set; }
        public string? from { get; set; }
        public object? from_user_id { get; set; }
        public string? _id { get; set; }
    }

    public class Root
    {
        public string? type { get; set; }
        public List<Message>? message { get; set; }
        public string? event_id { get; set; }
    }

    public class To
    {
        public string? name { get; set; }
    }
}

