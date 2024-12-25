using soundyard.club.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace soundyard.club.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        public AuthController()
        {
            _context = new AppDbContext();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(UserForRegistration userForRegistration)
        {
            if (ModelState.IsValid)
            {
                
                User user = new User
                {
                    FirstName = userForRegistration.FirstName,
                    LastName = userForRegistration.LastName,
                    Email = userForRegistration.Email,
                    Role = "User" ,
                    Password = userForRegistration.Password
                };

                if (user.Role == "Admin")
                {
                    user.Agreement = "Jsem klasický uživatel, a souhlasím s podmínkamy používání platformy";
                }
                else
                {
                    user.Agreement = "Jsem Admin , a souhlasím s podmínkamy spravování platformy";
                }

                _context.Users.Add(user);
                

                
                    _context.SaveChanges();
                

                return RedirectToAction("Login");
            }

            return View(userForRegistration);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserForLogin userForLogin)
        {
            if (ModelState.IsValid)
            {
                
                User user = _context.Users.FirstOrDefault(u => u.Email == userForLogin.Email);

                
                if (user != null && (user.Password == userForLogin.Password))   
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Dashboard", "Dash");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid password.");
                }

                
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }

            return View(userForLogin);
        }

    }
}