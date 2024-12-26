using ChatModels;
using Microsoft.EntityFrameworkCore;

namespace DemoChatApp.Data
{
    public class DbContextOptions(DbContextOptions<DbContextOptions> options) : DbContext(options)
    {
        
  

    public DbSet<Chat> Chats { get; set; }
    }
}
