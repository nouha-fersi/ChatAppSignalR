using ChatModels;
using DemoChatApp.Client.DTOs;
using DemoChatApp.Client.Models;
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

        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            await chatRepo.AddAvailableUserASync(availableUser);
            
            await Clients.All.SendAsync("NotifyAllClients", await GetUsers());
        }

        public async Task RemoveUserAsync (string userId)
        {
            await chatRepo.RemoveUsersAsync(userId);
            await Clients.All.SendAsync("NotifyAllClients", await GetUsers());

        }

        private async Task<List<AvailableUserDTO>> GetUsers() => await chatRepo.GetAvailableUsersAsync();

           
    }
}
