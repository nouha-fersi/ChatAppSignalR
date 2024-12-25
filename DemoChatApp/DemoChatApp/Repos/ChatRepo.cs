using ChatModels;
using DemoChatApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoChatApp.Repos
{
    public class ChatRepo(AppDbContext appDbContext)
    {
        public async Task SaveChatAsync(Chat chat)
        {
            appDbContext.Chats.Add(chat);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<List<Chat>> GetChatsAsync() 
            => await appDbContext.Chats.ToListAsync();
    }
}
