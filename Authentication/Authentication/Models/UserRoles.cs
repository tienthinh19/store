
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
namespace Authentication.Models
{
    public class UserRoles
    {
        public IdentityUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}
