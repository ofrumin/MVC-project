using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BL;
using System.Net;

namespace Car_Rental_Site.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BranchesController : Controller
    {
        //Manage Branches - for the main admin page
        public ActionResult branchesIndex()
        {
            try
            {
                using (BranchLogic logic = new BranchLogic())
                {
                    //return PartialView("_BranchesPartial", logic.GetAllBranches());
                    return View("Branches", logic.GetAllBranches());
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                //return PartialView("_BranchesPartial", new List<Branch>());
                return View("Branches", new List<Branch>());
            }
        }

        //Create new Branch to add to the Branches - show only
        public ActionResult Create()
        {
            return View();
        }

        //Add the new Branch - actual action
        [HttpPost]
        public ActionResult Create(Branch Branch)
        {
            //try
            //{
            if (!ModelState.IsValid)
                return View();

            BranchLogic logic = new BranchLogic();

            logic.AddBranch(Branch);


            return RedirectToAction("Index", "MangeHome");
            //}
            //catch
            //{
            //    ViewBag.Error = "We have problem on the server, Please try again later!";
            //    return View(new Branch());
            //}
        }

        //Edit Branch page - show only
        public ActionResult Edit(int? BranchID)
        {
            try
            {
                using (BranchLogic logic = new BranchLogic())
                {
                    return View(logic.GetOneBranchDetails(BranchID));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new Branch());
            }
        }

        //Save the Changes - actual action
        [HttpPost]
        public ActionResult Edit(Branch Branch)
        {
            //try
            //{
            if (!ModelState.IsValid)
                return View();

            BranchLogic logic = new BranchLogic();

            logic.EditBranch(Branch);


            return RedirectToAction("Index", "MangeHome");
            //}
            //catch
            //{
            //    ViewBag.Error = "We have problem on the server, Please try again later!";
            //    return View(new Branch());
            //}
        }

        //Delete Branch from the Branches - show only
        public ActionResult Delete(int? BranchID)
        {
            try
            {
                using (BranchLogic logic = new BranchLogic())
                {
                    return View(logic.GetOneBranchDetails(BranchID));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new Branch());
            }
        }

        //Delete vehicle from the fleet - actual action
        [HttpPost]
        public ActionResult Delete(Branch Branch)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                BranchLogic logic = new BranchLogic();

                logic.DeleteBranch(Branch);


                return RedirectToAction("Index", "MangeHome");
            }
            catch
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new Branch());
            }
        }

        public ActionResult BranchList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string errorMessage = (from state in ModelState.Values
                                           from error in state.Errors
                                           select error.ErrorMessage).FirstOrDefault().ToString();
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errorMessage);
                }

                using (BranchLogic logic = new BranchLogic())
                {
                    var q = logic.GetAllBranches().Select(m => new
                    {
                        BranchID = m.BranchID,
                        DetailsString = m.BranchName + " " + m.Address
                    }).ToList();

                    return Json(q, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "We have problem on the server, Please try again later!");
            }
        }
    }
}
