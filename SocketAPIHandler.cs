using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SocketIOClient;

namespace TwitchFlashbang
{
    public class SocketAPIHandler
    {
        public static async Task startConnection()
        {
            var client = new SocketIO("https://sockets.streamlabs.com?token=$eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbiI6IjYxRjExNEM3MTQ2RUIwOUJEQzg4QjM2M0M3OUFFOUMwQjlENUQ1ODdCMEE3RkM2RTFGQTgxQzdBMTM2RjlFRUQxREVFMUVDODlEOTg4QTdCNkFCNDc1NDRGOTI3NDMwRDM5RjQ4NjI0NDQyMTEyRUUwRTlEQkRCODgxNjFFOUU1QUVGQTcwNjcxQkNDQzBFMUFFOEM2ODUxNDU0QTA5QTMzRTU1QUI1NkJGQjM5MkYwNUFFRkQ3RTRBREMxODg2QURBMDY5MTkxQUYzRkFBOTQ5MkMzMEZFQjExQ0Q1REJCODEwNzcwQzk2QUYzODNFMUJGOTA4RjA3RkEiLCJyZWFkX29ubHkiOnRydWUsInByZXZlbnRfbWFzdGVyIjp0cnVlLCJ0d2l0Y2hfaWQiOiI1ODc0MzY1OTUifQ.jo55U9kPirljg1rWogbehWU6nqXXt2DroANeDab7OmQ");
            client.On("event", response =>
            {
               if (MainWindow.gameOverlay != null) { MainWindow.gameOverlay.CSGOflash(); }
            });

            await client.ConnectAsync();
        }
    }
}