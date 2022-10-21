using SocketIOClient;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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
}

