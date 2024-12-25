using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using club.soundyard.web.Models;
namespace club.soundyard.web.Controllers
{
    public class DashController : Controller
    {
        AppDbContext _context;
        public DashController()
        {
            _context = new AppDbContext();
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                return RedirectToAction("Dashboard", "Dash");
            }
        }

       
        public ActionResult Dashboard()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }
            string userEmail = User.Identity.Name;
            User user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            
            ViewBag.Agreement = user?.Agreement;  
            return View();

            
        }

        [Authorize]
        public ActionResult Agreement()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();  
            return RedirectToAction("Login", "Auth");  
        }


    }
}