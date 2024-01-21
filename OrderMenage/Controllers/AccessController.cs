using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OrderMenage.Models;

namespace OrderMenage.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login() 
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Order");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login modelLogin)
        {
            if(modelLogin.Name == "admin" && modelLogin.PassWord == "admin")
            {
               List<Claim> claims = new List<Claim>()
               {
                   new Claim(ClaimTypes.NameIdentifier, modelLogin.Name),
                   new Claim("OtherProperties", "Przykładowa Rola")
               };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Order");


            }

            ViewData["ValidateMessage"] = "Niema takiego użytkownika";
            return View();
        }

    }
}

