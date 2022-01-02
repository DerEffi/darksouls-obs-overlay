using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DarkSoulsOBSOverlay.Services
{
    public static class CommunicationService
    {
        private static readonly List<WebSocket> Clients = new();

        public static void SendMessage(string message, bool throws = true)
        {

            Clients.RemoveAll(c =>
            {
                if (c.State == WebSocketState.Open || c.State == WebSocketState.Connecting)
                    return false;
                c.Dispose();
                return true;
            });

            Clients.ForEach(async c =>
            {
                try
                {
                    var segments = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                    await c.SendAsync(segments, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch { }
            });
        }

        public static async Task AddClient(HttpContext context, WebSocket webSocket)
        {
            Clients.Add(webSocket);
            DarkSoulsReader.ClearStats();

            ArraySegment<byte> message = new ArraySegment<byte>(new byte[4096]);
            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    await webSocket.ReceiveAsync(message, CancellationToken.None);
                }
                catch {}
            }
        }
    }
}
