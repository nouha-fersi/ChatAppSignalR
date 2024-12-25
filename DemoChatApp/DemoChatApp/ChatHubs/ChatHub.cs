using ChatModels;
using Microsoft.AspNetCore.SignalR;

namespace DemoChatApp.ChatHubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Chat chat)
            => await Clients.All.SendAsync("ReceiveMessage",chat);
    }
}
