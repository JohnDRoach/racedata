using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaceDataHome.Models;

namespace RaceDataHome.Controllers
{
    public class HomeController : Controller
    {
        private ContactDetails contactDetails = new ContactDetails();

        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your quintessential contact page.";

            return View(contactDetails);
        }
    }
}
