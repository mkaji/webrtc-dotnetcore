using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace webrtc_dotnetcore.Hubs
{
    public class WebRTCHub : Hub
    {
        //https://docs.microsoft.com/en-us/aspnet/core/signalr/groups?view=aspnetcore-3.0

        private static int clientCount = 0;

        public async Task CreateOrJoin(string room)
        {
            Debug.WriteLine("Received request to create or join room " + room);
            Debug.WriteLine("Room now has " + clientCount + " client(s)");

            if (clientCount == 1)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                Debug.WriteLine("Client ID " + Context.ConnectionId + " created room " + room);
                await Clients.Caller.SendAsync("created", room, Context.ConnectionId);
            }
            else if (clientCount == 2)
            {
                Debug.WriteLine("Client ID " + Context.ConnectionId + " joined room " + room);
                //await Clients.Group(room).SendAsync("join", room);
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                await Clients.Caller.SendAsync("joined", room, Context.ConnectionId);
                await Clients.Group(room).SendAsync("ready");
            }
            else
            {
                Debug.WriteLine("full");
                await Clients.Caller.SendAsync("full", room);
            }
        }

        public async Task message(object message)
        {
            try
            {
                Debug.WriteLine("message:" + message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            await Clients.AllExcept(Context.ConnectionId).SendAsync("message", message);
        }

        //public async Task ipaddr(string room)
        //{
        //    Debug.WriteLine("ipaddr");

        //}

        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("OnConnectedAsync");
            clientCount++;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Debug.WriteLine("OnDisconnectedAsync");
            clientCount--;
            Clients.AllExcept(Context.ConnectionId).SendAsync("bye");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
