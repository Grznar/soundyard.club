using soundyard.club.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Register(UserForRegistration userForRegistration)
        {
            if (ModelState.IsValid)
            {
                
                User user = new User
                {
                    FirstName = userForRegistration.FirstName,
                    LastName = userForRegistration.LastName,
                    Email = userForRegistration.Email,
                    Role = "User" 
                };

                
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
        public ActionResult Login(UserForLogin userForLogin)
        {
            if (ModelState.IsValid)
            {
                
                User user = _context.Users.FirstOrDefault(u => u.Email == userForLogin.Email);

                
                if (user != null && userForLogin.Password == "testpassword")    
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                
                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(userForLogin);
        }

    }
}