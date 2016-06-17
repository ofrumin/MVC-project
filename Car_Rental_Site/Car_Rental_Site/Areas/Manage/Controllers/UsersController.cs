using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Model;
using Car_Rental_Site.Areas.Manage.Models;



 


namespace Car_Rental_Site.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        //Manage Users - for the main admin page
        public ActionResult UsersIndex()
        {
            try
            {
                using (UsersLogic logic = new UsersLogic())
                {
                    return View("Users", logic.GetAllUsers());

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View("Partial", new List<User>());
            }
        }


        //Edit user page - show only
        public ActionResult Edit(string userName)
        {
            try
            {
                using (UsersLogic logic = new UsersLogic())
                {
                    return View(logic.GetOneUserDetails(userName));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new User());
            }
        }

        //Save the Changes - actual action
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (UsersLogic logic = new UsersLogic())
                {
                    logic.EditUser(user);
                }
                return RedirectToAction("Index", "MangeHome");
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new User());
            }
        }

        //Delete User page - show only
        public ActionResult Delete(string userName)
        {
            try
            {
                using (UsersLogic logic = new UsersLogic())
                {
                    return View(logic.GetOneUserDetails(userName));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new User());
            }
        }

        //Delete the User - actual action
        [HttpPost]
        public ActionResult Delete(User user)
        {
            try
            {
                using (UsersLogic logic = new UsersLogic())
                {
                    logic.DeleteUser(user);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new User());
            }
        }

        /// <summary>
        /// Reset user password - for admin only
        /// No need to know the old password
        /// </summary>
        /// <param name="model">The new password and the user name</param>
        /// <returns>to ajax Ok/BadRequest</returns>
        //[HttpPut]
        //public ActionResult ResetPassword(ResetPassword model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        string errorMessage = (from state in ModelState.Values
        //                               from error in state.Errors
        //                               select error.ErrorMessage).FirstOrDefault().ToString();
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errorMessage);
        //    }

        //    try
        //    {
        //        using (UsersLogic logic = new UsersLogic())
        //        {
        //            string cypherNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "sha1");
        //            if (logic.ResetPasswordManager(User.Identity.Name, cypherNewPassword) > 0)
        //                return new HttpStatusCodeResult(HttpStatusCode.OK);
        //            else
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "we have problems with the server!");
        //    }
        //}
        
            public ActionResult Index()
            {
                return View();
            }

            [Authorize(Roles = "Admin")]
            public ActionResult Manage()
            {
                Manager um = new Manager();
                IEnumerable<User> users = um.GetAllUsers();
                return View(users);
            }

            [Authorize(Roles = "Admin")]
            public ActionResult GetRolesForUser(int userId)
            {
                Manager um = new Manager();
                ExRoleProvider rp = new ExRoleProvider();
                try
                {
                    User u = um.GetUserById(userId);
                    string[] allRoles = rp.GetAllRoles();
                    string[] roles = rp.GetRolesForUser(u.UserName);
                    IEnumerable<UserRoleVM> roleAssignments = allRoles.Select(r => new UserRoleVM() { RoleName = r, AssignedToRole = roles.Contains(r) });
                    return Json(new { Status = "success", Data = roleAssignments }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Status = "failed" }, JsonRequestBehavior.AllowGet);
                }
            }

            private ActionResult ChangeRoleToUser(int userId, string roleName, Action<string[], string[]> operation)
            {
                Manager um = new Manager();
                try
                {
                    User u = um.GetUserById(userId);
                    operation(new[] { u.UserName }, new[] { roleName });
                    return Json(new { Status = "success" });
                }
                catch (Exception)
                {
                    return Json(new { Status = "failed" });
                }
            }

            [HttpPost]
            [Authorize(Roles = "Admin")]
            public ActionResult AddRoleToUser(int userId, string roleName)
            {
                return ChangeRoleToUser(userId, roleName, new ExRoleProvider().AddUsersToRoles);
            }

            [HttpPost]
            [Authorize(Roles = "Admin")]
            public ActionResult RemoveRoleFromUser(int userId, string roleName)
            {
                return ChangeRoleToUser(userId, roleName, new ExRoleProvider().RemoveUsersFromRoles);
            }
            public string GetUserRole(string userName)
            {
                try
                {
                    using (AccountLogic logic = new AccountLogic())
                    {
                        return logic.userRole(userName);
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "We have problem on the server, Please try again later!";
                    ViewBag.CriticalError = true;
                    return "Problem";
                }
            }
    
    }
}
