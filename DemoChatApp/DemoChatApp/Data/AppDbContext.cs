using ChatModels;
using Microsoft.EntityFrameworkCore;

namespace DemoChatApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        
  

    public DbSet<Chat> Chats { get; set; }
    }
}
