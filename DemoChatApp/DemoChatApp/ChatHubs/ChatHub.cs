using ChatModels;
using ChatModels.DTOs;
using ChatModels.Models;
using DemoChatApp.Repos;
using Microsoft.AspNetCore.SignalR;

namespace DemoChatApp.ChatHubs
{
    public class ChatHub (ChatRepo chatRepo) : Hub
    {
        public async Task SendMessageToGroup(GroupChat chat)
        {
           var saveChatDTO = await chatRepo.AddChatToGroupAsync(chat);
            await Clients.All.SendAsync("ReceiveGroupMessages", saveChatDTO);
           
        }

        public async Task AddAvailableUser(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            var availableUsers = await chatRepo.AddAvailableUserAsync(availableUser);
            
            await Clients.All.SendAsync("NotifyAllClients", availableUsers);
        }

        public async Task RemoveUser(string userId)
        {
            await chatRepo.RemoveUserAsync(userId);
            await Clients.All.SendAsync("NotifyAllClients", await GetUsers());

        }

        public async Task AddIndividualChat(IndividualChat individualChat)
        {
            Console.WriteLine("acdd indiv chat");
            await chatRepo.AddIndividualChatAsync(individualChat);
            var requestDTO = new RequestChatDTO()
            { ReceiverId = individualChat.ReceiverId , SenderId=individualChat.SenderId};
            var getChats = await chatRepo.GetIndividualChatsAsync(requestDTO);
            var prepareIndividualChat = new IndividualChatDTO()
            {
                SenderId = individualChat.SenderId,
                ReceiverId = individualChat.ReceiverId,
                Message = individualChat.Message,
                Date = individualChat.Date,
                ReceiverName = getChats.Where(_ => _.ReceiverId == individualChat.ReceiverId).FirstOrDefault()!.ReceiverName,
                SenderName = getChats.Where(_ => _.SenderId == individualChat.SenderId).FirstOrDefault()!.SenderName,
            };
            await Clients.User(individualChat.ReceiverId!).SendAsync("ReceiveIndividualMessage",prepareIndividualChat);
        }
        

        private async Task<List<AvailableUserDTO>> GetUsers() => await chatRepo.GetAvailableUsersAsync();

           
    }
}
