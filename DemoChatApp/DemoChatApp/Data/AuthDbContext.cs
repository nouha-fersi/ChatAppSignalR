using ChatModels;
using ChatModels.Models;
using DemoChatApp.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DemoChatApp.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<GroupChat> GroupChats { get; set; }
    }
}
