using ChatModels;
using DemoChatApp.Authentication;
using DemoChatApp.Client.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoChatApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


    public DbSet<Chat> Chat { get; set; }
    public DbSet<AvailableUser> AvailableUsers { get; set; }
    
    }
}
    

