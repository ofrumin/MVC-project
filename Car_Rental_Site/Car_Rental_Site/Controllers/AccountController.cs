using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car_Rental_Site.Models;
using BL;
using System.Web.Security;
using System.Threading;
using System.Net;



namespace Car_Rental_Site.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult NotAuthorized()
        {
            return View();
        }

        public ActionResult SignOut(string returnUrl)
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl + (!string.IsNullOrEmpty(returnUrl) ? "?returnUrl=" + Url.Encode(returnUrl) : ""));
        }

        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NotAuthorized", new { returnUrl = returnUrl });
            }
            return View(new UserLoginVM());
        }

        [HttpPost]
        public ActionResult Login(UserLoginVM userLogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Manager um = new Manager();
                if (um.ValidateUser(userLogin.Username, userLogin.Password))
                {
                    FormsAuthentication.SetAuthCookie(userLogin.Username, userLogin.PersistentLogin);
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? FormsAuthentication.DefaultUrl : returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username or password");
                }
            }
            return View(userLogin);
        }

        public ActionResult Register()
        {
            return View(new UserRegistrationVM());
        }

        [HttpPost]
        public ActionResult Register(UserRegistrationVM userRegistration)
        {
            if (ModelState.IsValid)
            {
                Manager um = new Manager();
                ReaderWriterLock locker = UsernamesChecks.GetUsernamesChecksReaderWriterLocker(HttpContext.Application);
                try
                {
                    locker.AcquireWriterLock(-1);
                    try
                    {
                        if (UsernamesChecks.IsUsernameReserved(HttpContext.Application, Session, userRegistration.Username))
                        {
                            throw new InvalidOperationException("The selected username already exists");
                        }
                        um.SaveNewUser(userRegistration.GetUser());
                        UsernamesChecks.ReleaseUsernameFromReservedUsernamesCollection(HttpContext.Application, Session, userRegistration.Username);
                        TempData["succesfulRegistration"] = true;
                        return RedirectToAction("Login");
                    }
                    finally
                    {
                        locker.ReleaseWriterLock();
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View(userRegistration);
        }

        

        //User manage page - the user can see is details and is orders in this page
        public ActionResult ManageAccount()
        {
            try
            {
                using (UsersLogic logic = new UsersLogic())
                {
                    return View(logic.GetOneUserDetails(User.Identity.Name));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View();
            }
        }

        //return to ajax the user orders / BadRequest
        public ActionResult UserOrders()
        {
            try
            {
            LeasedVehicleLogic logic = new LeasedVehicleLogic();
            

            return Json(logic.UserOrders(User.Identity.Name), JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "We have problem on the server, Please try again later!");
            }
        }

    }
}
