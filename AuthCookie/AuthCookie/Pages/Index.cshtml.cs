using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthCookie.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthCookie.Pages
{
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public string Message { get; private set; } = string.Empty;

        public IndexModel(
            IUserService userService
        )
        {
            _userService = userService;
        }

        [BindProperty]
        public Model.User Customer { get; set; }

        public void OnGet()
        {

            if (User.Identity.IsAuthenticated)
            {
                Message += "Olá Usuário, você está autenticado";

            }
            else
            {
                Message += "Você não está autenticado";
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Index");
            }


            var user = _userService.Authenticate(Customer.Email, Customer.Password);

            if (user == null)
                return Unauthorized();


            var claim = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Token.ToString())
                });

            var claims = new[]
            {
                 new Claim(ClaimTypes.Email, user.Email.ToString())
            }.ToAsyncEnumerable().ToEnumerable();

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            // return basic user info (without password) and token to store client side
            return RedirectToPage("/Index");
        }
    }
}
