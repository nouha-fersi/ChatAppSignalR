using DemoChatApp.Authentication;
using ChatModels.DTOs;
using ChatModels.Models;
using DemoChatApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoChatApp.Repos
{
    public class ChatRepo(AppDbContext appDbContext, UserManager<AppUser> userManager)
    {
      public async Task<GroupChatDTO> AddChatToGroupAsync(GroupChat chat)
        {
            var entity = appDbContext.GroupChats.Add(chat).Entity;
            await appDbContext.SaveChangesAsync();
            return new GroupChatDTO()
            {
                SenderId = entity.SenderId,
                SenderName = (await userManager.FindByIdAsync(entity.SenderId!))!.Fullname,
                DateTime = entity.DateTime,
                Id = entity.Id,
                Message = entity.Message,

            };
        }

        public async Task<List<GroupChatDTO>> GetGroupChatsAsync()
        {
            var List = new List<GroupChatDTO>();
            var chats = await appDbContext.GroupChats.ToListAsync();
            foreach (var c in chats)
            {
                List.Add(new GroupChatDTO()
                {
                    SenderId = c.SenderId,
                    DateTime = c.DateTime,
                    Id = c.Id,
                    Message = c.Message,
                    SenderName = (await userManager.FindByIdAsync(c.SenderId!))!.Fullname,
                });
            }
            return List;
        }



        public async Task<List<AvailableUserDTO>> AddAvailableUserAsync(AvailableUser availableUser)
        {
            var List = new List<AvailableUserDTO>();
            var getUser = await appDbContext.AvailableUsers
                .FirstOrDefaultAsync(_ => _.UserId == availableUser.UserId);
            if (getUser != null)
            {
                getUser.ConnectionId = availableUser.ConnectionId;
            }
            else
            {
                appDbContext.AvailableUsers.Add(availableUser);
            }
            await appDbContext.SaveChangesAsync();

            var allUser = await appDbContext.AvailableUsers.ToListAsync();
            foreach (var user in allUser)
            {
                List.Add(new AvailableUserDTO()
                {
                    UserId = user.UserId,
                    Fullname = (await userManager.FindByIdAsync(user.UserId))!.Fullname,
                });
            }
            return List;
        }

        public async Task<List<AvailableUserDTO>> GetAvailableUsersAsync()
        {
            var List = new List<AvailableUserDTO>();
            var users = await appDbContext.AvailableUsers.ToListAsync();

            foreach (var u in users)
            {
                List.Add(new AvailableUserDTO()
                {
                    UserId = u.UserId,
                    Fullname = (await userManager.FindByIdAsync(u.UserId))!.Fullname,
                });
            }

            return List;
        }

        public async Task<List<AvailableUserDTO>> RemoveUserAsync(string userId)
        {
            var user = await appDbContext.AvailableUsers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                appDbContext.AvailableUsers.Remove(user);
                await appDbContext.SaveChangesAsync();
            }

            var List = new List<AvailableUserDTO>();
            var users = await appDbContext.AvailableUsers.ToListAsync();

            foreach (var u in users)
            {
                List.Add(new AvailableUserDTO()
                {
                    UserId = u.UserId,
                    Fullname = (await userManager.FindByIdAsync(u.UserId))!.Fullname
                });
            }

            return List;
        }

        public async Task AddIndividualChatAsync (IndividualChat individualChat)
        {
            appDbContext.IndividualChats.Add(individualChat);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<List<IndividualChatDTO>> GetIndividualChatsAsync (RequestChatDTO requestChatDTO)
        {
            var ChatList = new List<IndividualChatDTO>();
            var chats = await appDbContext.IndividualChats
        .Where(s => (s.SenderId == requestChatDTO.SenderId && s.ReceiverId == requestChatDTO.ReceiverId) ||
                    (s.SenderId == requestChatDTO.ReceiverId && s.ReceiverId == requestChatDTO.SenderId))
        .ToListAsync();

            if (chats != null)
            {
                foreach (var chat in chats)
                {
                    ChatList.Add(new IndividualChatDTO()
                    {
                        SenderId = chat.SenderId,
                        ReceiverId = chat.ReceiverId,
                        SenderName = (await userManager.FindByIdAsync(chat.SenderId!))!.Fullname,
                        ReceiverName = (await userManager.FindByIdAsync(chat.ReceiverId!))!.Fullname,
                        Message = chat.Message,
                        Date = chat.Date,
                    });
                }
                return ChatList;
            }
            else
            {
                return null!;
            }
        }


    }
}
