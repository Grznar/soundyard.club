using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace soundyard.club.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            string userEmail = HttpContext.User.Identity.Name;

            return View();
        }

        

    }
}