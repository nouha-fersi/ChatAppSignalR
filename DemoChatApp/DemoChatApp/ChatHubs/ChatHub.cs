using ChatModels;
using DemoChatApp.Repos;
using Microsoft.AspNetCore.SignalR;

namespace DemoChatApp.ChatHubs
{
    public class ChatHub (ChatRepo chatRepo) : Hub
    {
        public async Task SendMessage(Chat chat)
        {
            await chatRepo.SaveChatAsync(chat);
            await Clients.All.SendAsync("ReceiveMessage",chat);
           
        }
           
    }
}
