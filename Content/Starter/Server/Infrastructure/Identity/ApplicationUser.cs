using Microsoft.AspNetCore.Identity;

namespace Server.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string PictureUri { get; set; }
    }
}
