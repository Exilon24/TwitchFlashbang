using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using SocketIOClient;

namespace TwitchFlashbang
{
    public class SocketAPIHandler
    {
        public static async Task startConnection()
        {
            // Create the socket object.
            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbiI6IjM5ODE5NTZGQTVGRDRBQTQ5NDY2MDE1QkUzNTM5MUE0MTRFN0I4RUYyMDRENDk5Mjk1NzFEOTY3NDVDMjkyNDJBRUZFMDYxMDVDQjRFQUQyNDQyRkMxRjJCMEE1QzE1N0VDOEUxODcyRThDNDhCM0QyMUY0RERBQjk2MjMxRDk3NEIzODcwMUE4MzIwODZERjIxQ0M2MkY5QjZFRjU0ODlDNUZGNTAxMDQ4NjQyQzFFMjMzOUVDQzdCNTE2MDIyREU4RkU0OTQ3NkVDNDBDMDIyMzVCMTQyNEY1NEVERDk1Njc2MTI0OTk0NkI2MkNBOUE1NkI3RkVENzMiLCJyZWFkX29ubHkiOnRydWUsInByZXZlbnRfbWFzdGVyIjp0cnVlLCJ0d2l0Y2hfaWQiOiI1ODc0MzY1OTUifQ.A2Bh_zcYf7MGXewwJ7ORpvz9eILtKUdoJyUpkLZS1_o";
            var sio = new SocketIO($"https://sockets.streamlabs.com?token={token}", new SocketIOOptions()
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
                Dispatcher.Invoke(() =>
                {
                    MainWindow.socketConnectionStatus.Text = "Status: Connected";
                    MainWindow.socketConnectionStatus.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                });
            };

            sio.On("event", data =>
            {
                MainWindow.gameOverlay.CSGOflash();
            });

            await sio.ConnectAsync();
            Console.ReadKey(true);


        }
    }
}