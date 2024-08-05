using Microsoft.AspNetCore.Identity;

namespace ManagingUser.Models
{
    public class UserRoles
    {
        public IdentityUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}
