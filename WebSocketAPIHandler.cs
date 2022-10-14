using System;
using System.Diagnostics;
using WebSocketSharp;

namespace TwitchFlashbang
{
    public class WebSocketAPIHandler
    {
        public static void startConnection()
        {
            var ws = new WebSocket(@"wss://demo.piesocket.com/v3/channel_1?api_key=VCXCEuvhGcBDP7XhiJJUDvR1e1D3eiVjgZ9VRiaV&notify_self");
            ws.OnMessage += Ws_OnMessage;
            ws.Connect();
           
        }

        private static void Ws_OnMessage(object? sender, MessageEventArgs e)
        {
            if (MainWindow.gameOverlay != null) { MainWindow.gameOverlay.CSGOflash(); }
        }
    }
}