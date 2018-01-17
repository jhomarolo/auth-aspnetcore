using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthCookie.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
