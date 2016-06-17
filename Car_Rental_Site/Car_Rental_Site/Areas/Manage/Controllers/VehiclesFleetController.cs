using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BL;
using Car_Rental_Site.Areas.Manage.Models;

namespace Car_Rental_Site.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehiclesFleetController : Controller
    {
        //Manage Vehicles Fleet - for the main admin page
        public ActionResult vehiclesFleetIndex()
        {
            try
            {
                using (VehiclesFleetLogic logic = new VehiclesFleetLogic())
                {

                    return View("VehiclesFleet", logic.GetAllVehiclesFleet());
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View("VehiclesFleet", new List<VehiclesFleet>());
            }
        }

        //Create new vehicle to add to the fleet - show only
        public ActionResult Create()
        {
            return View();
        }

        //Add the new vehicle to the fleet - actual action
        [HttpPost]
        public ActionResult Create(VehiclesFleet car)
        {
            try
            {
                #region TypeToAdd
                VehiclesType typeToAdd = new VehiclesType();
                VehiclesTypesLogic logicType = new VehiclesTypesLogic();
                var type = logicType.GetOneVehicleTypeDetails(car.TypeID);


                typeToAdd.DailyRentCost = type.DailyRentCost;
                typeToAdd.Manufacturer = type.Manufacturer;
                typeToAdd.ManufactureYear = type.ManufactureYear;
                typeToAdd.Model = type.Model;
                typeToAdd.Picture = type.Picture;
                typeToAdd.RentCostForDelay = type.RentCostForDelay;
                typeToAdd.Transmission = type.Transmission;

                car.Type = typeToAdd;
                #endregion

                VehiclesFleetLogic numToCheck = new VehiclesFleetLogic();
                if (!numToCheck.CheckVehicleNumber(car.CarNumber))
                {
                    ModelState.AddModelError("CarNumber", "The number already exists!");
                }

                if (!ModelState.IsValid)
                    return View();

                using (VehiclesFleetLogic logic = new VehiclesFleetLogic())
                {
                    logic.AddVehiclesToFleet(car);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch
            {
                ViewBag.Error = "We have problem on the server, Please try again!";
                return View(new VehiclesFleet());
            }
        }

        //Edit vehicle page - show only
        public ActionResult Edit(string carNumber)
        {
            try
            {
                using (VehiclesFleetLogic logic = new VehiclesFleetLogic())
                {
                    return View(logic.GetOneCarDetails(carNumber));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new VehiclesFleet());
            }
        }

        //Save the Changes - actual action
        [HttpPost]
        public ActionResult Edit(VehiclesFleet car)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (VehiclesFleetLogic logic = new VehiclesFleetLogic())
                {
                    logic.EditVehiclesFromFleet(car);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new VehiclesFleet());
            }
        }

        //Delete vehicle from the fleet - show only
        public ActionResult Delete(string carNumber)
        {
            try
            {
                using (VehiclesFleetLogic logic = new VehiclesFleetLogic())
                {
                    return View(logic.GetOneCarDetails(carNumber));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new VehiclesFleet());
            }
        }

        //Delete vehicle from the fleet - actual action
        [HttpPost]
        public ActionResult Delete(VehiclesFleet car)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (VehiclesFleetLogic logic = new VehiclesFleetLogic())
                {
                    logic.DeleteVehiclesFromFleet(car);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new VehiclesFleet());
            }
        }


    }
}
