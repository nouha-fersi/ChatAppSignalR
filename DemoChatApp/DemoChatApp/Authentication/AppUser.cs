using Microsoft.AspNetCore.Identity;

namespace DemoChatApp.Authentication
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
