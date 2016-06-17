using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Rental_Site.Controllers
{
    public class HomeController : Controller
    {
        //Home page view
        public ActionResult Index()
        {
            return View();
        }

        //About page view
        public ActionResult About()
        {
            return View();
        }

        //Contact page view
        public ActionResult Contact()
        {
            return View();
        }

        //If user try to get to page he don't have access to
        //Redirect the user to another page
        //Redirect to login only if the user don't login
        //Admin role can view any page so he never get here
        public ActionResult redirectToLogin()
        {
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("index", "ReturnVehicle", new { Area = "Employee", Controller = "ReturnVehicle", action = "index" });
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("Manage", "Account");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

    }
}
