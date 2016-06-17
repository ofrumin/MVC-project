using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Model;

namespace Car_Rental_Site.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class ReturnVehicleController : Controller
    {
        //Return vehicle main page
        public ActionResult Index()
        {
            try
            {
                using (EmployeeLogic logic = new EmployeeLogic())
                {
                    return View(logic.LeasededVehicles());
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View();
            }
        }

        //After the vehicle as return - this page show how much the user need to pay and for how much days he rent the vehicle
        public ActionResult ReturnVehicle(int? rentalID)
        {
            try
            {
                using (EmployeeLogic logic = new EmployeeLogic())
                {
                    var q = logic.ReturnVehicle(rentalID);
                    if (q != null)
                    {
                        return View(logic.ReturnVehicle(rentalID));
                    }
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View();
            }
        }
        //Edit LeasedVehicle page - show only
        public ActionResult Edit(int id)
        {
            //try
            //{
            LeasedVehicleLogic logic = new LeasedVehicleLogic();

            return View(logic.GetOneLeasedVehicleDetails(id));

            //}
            //catch (Exception)
            //{
            //    ViewBag.Error = "We have problem on the server, Please try again later!";
            //    ViewBag.CriticalError = true;
            //    return View(new VehiclesType());
            //}
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

            return RedirectToAction("Index", "ReturnVehicle");
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new LeasedVehicle());
            }
        }

    }
}
