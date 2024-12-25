using club.soundyard.web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace club.soundyard.web.Controllers
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
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userForRegistration.Password);
                User user = new User
                {
                    FirstName = userForRegistration.FirstName,
                    LastName = userForRegistration.LastName,
                    Email = userForRegistration.Email,
                    Role = "User" ,
                    Password = hashedPassword
                };
                if (user.Email.ToLower().Contains("@soundyard"))
                    {
                    user.Role = "Admin";
                    }

                if (user.Role == "Admin")
                {
                    user.Agreement = "Admin";
                }
                else
                {
                    user.Agreement = " just a User";
                }

                _context.Users.Add(user);

                try
                {

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

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

                
                if (user != null)   
                {
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userForLogin.Password, user.Password);
                    if (isPasswordValid)
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, false);
                        return RedirectToAction("Dashboard", "Dash");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid password.");
                    }
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