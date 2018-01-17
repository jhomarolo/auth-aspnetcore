using AuthCookie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthCookie.Service
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }

    public class UserService : IUserService
    {
        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            User user = new User { Email = email, Token = "Token", Name = "Usuário Codigo Simples" };

            return user;
        }
    }
}