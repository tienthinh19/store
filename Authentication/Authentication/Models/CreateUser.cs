using System;
using System.Collections.Generic;
namespace Authentication.Models
{
    public class CreateUser
    {
        public CreateUser()
        {
            Roles = new List<string>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
