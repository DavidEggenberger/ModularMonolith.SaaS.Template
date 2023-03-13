using Microsoft.AspNetCore.Identity;

namespace Server.Modules.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string PictureUri { get; set; }
    }
}
