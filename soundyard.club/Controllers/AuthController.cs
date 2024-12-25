using club.soundyard.web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
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
                    user.Agreement = "User";
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
                SendActivationEmail(userForRegistration.Email);
                return RedirectToAction("Login");
            }

            return View(userForRegistration);
        }
        private void SendActivationEmail(string receiver)
        {
            string subject = "Activation Email";
            string body = "Click on the following link to activate your account.";

            try
            {
                MailMessage mailMessage = new MailMessage("noreply@gmail.com", receiver, subject, body);
                SmtpClient smtpClient = new SmtpClient("212.71.162.103",587);
                






                smtpClient.Send(mailMessage);
                Console.WriteLine("Email send");
            }
            catch (SmtpException ex)
            {
                
                Console.WriteLine(ex.Message);
                Console.WriteLine("SMTP Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);

                // Případně vypište i další vlastnosti výjimky
                Console.WriteLine("Status Code: " + ex.StatusCode);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                // Zachytí obecné chyby, které nejsou SmtpException
                Console.WriteLine("General Exception: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }
            
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