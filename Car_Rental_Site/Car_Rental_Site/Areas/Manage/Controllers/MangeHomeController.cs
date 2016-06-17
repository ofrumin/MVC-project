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
    public class MangeHomeController : Controller
    {
        //Admin main page
        public ActionResult Index()
        {
            return View();
        }

        //Leased vehicle - for the main page
        //The mange of this category is in the worker area
        public ActionResult leasedVehicleIndex()
        {
            try
            {
                LeasedVehicleLogic logic = new LeasedVehicleLogic();
                return View("LeasedVehicle", logic.GetAllVehicles());

            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View("LeasedVehicle", new List<LeasedVehicle>());
            }
        }



        //Edit LeasedVehicle page - show only
        public ActionResult Edit(int id)
        {
            try
            {
            LeasedVehicleLogic logic = new LeasedVehicleLogic();

            return View(logic.GetOneLeasedVehicleDetails(id));

            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new VehiclesType());
            }
        }

        //Save the Changes - actual action
        [HttpPost]
        public ActionResult Edit(LeasedVehicle editDate)
        {
            try
            {
            if (!ModelState.IsValid)
                return View();

            LeasedVehicleLogic logic = new LeasedVehicleLogic();
            logic.EditLeasedVehicles(editDate);

            return RedirectToAction("Index", "MangeHome");
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new LeasedVehicle());
            }
        }

        //Delete LeasedVehicle page - show only
        public ActionResult Delete(int? id)
        {
            try
            {
                using (LeasedVehicleLogic logic = new LeasedVehicleLogic())
                {
                    return View(logic.GetOneLeasedVehicleDetails(id));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new LeasedVehicle());
            }
        }

        //Delete LeasedVehicle - actual action
        [HttpPost]
        public ActionResult Delete(LeasedVehicle deleteLeasedVehicle)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (LeasedVehicleLogic logic = new LeasedVehicleLogic())
                {
                    logic.DeleteLeasedVehicles(deleteLeasedVehicle);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new LeasedVehicle());
            }
        }

    }
}
