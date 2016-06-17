using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Model;
using System.Net;

namespace Car_Rental_Site.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehiclesTypesController : Controller
    {
        //Manage Vehicles Types - for the main admin page
        public ActionResult vehiclesTypesIndex()
        {
            try
            {
                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                   
                    return View("VehiclesTypes", logic.GetAllVehiclesTypes());
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                
                return View("VehiclesTypes", new List<VehiclesType>());
            }
        }

        /// <summary>
        /// Get all vehicle types as list of string
        /// </summary>
        /// <returns>To ajax The list of all vehicle types/BadRequest</returns>
        public ActionResult VehiclesTypesList()
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

                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                    var q = logic.GetAllVehiclesTypes().Select(m => new
                    {
                        VehicleTypeID = m.VehicleTypeID,
                        DetailsString = m.VehicleTypeID + " " + m.Manufacturer + " " +
                        m.Model + " " + m.ManufactureYear + " " +
                        m.Transmission
                    }).ToList();

                    return Json(q, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "We have problem on the server, Please try again later!");
            }
        }

        public ActionResult VehiclesTypesIDList()
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

                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                    var q = logic.GetAllVehiclesTypes().Select(m => new
                    {
                        Manufacturer=m.Manufacturer,
                        VehicleTypeID = m.VehicleTypeID,
                        Model=m.Model,
                        Transmission=m.Transmission,
                        DailyRentCost=m.DailyRentCost,
                        RentCostForDelay=m.RentCostForDelay,
                        Picture=m.Picture,

                        DetailsString = m.Manufacturer + " " +
                        m.Model + " " + m.ManufactureYear + " " +
                        m.Transmission

                    }).ToList();

                    return Json(q, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "We have problem on the server, Please try again later!");
            }
        }

        //Create new vehicle type - show only
        public ActionResult Create()
        {
            return View();
        }

        //Create the new vehicle type - actual action
        [HttpPost]
        public ActionResult Create(VehiclesType newType)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                    logic.AddVehicleType(newType);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View();
            }
        }

        //Edit vehicle type page - show only
        public ActionResult Edit(int id)
        {
            try
            {
                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                    return View(logic.GetOneVehicleTypeDetails(id));
                }
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
        public ActionResult Edit(VehiclesType editType)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                    logic.EditVehicleType(editType);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new VehiclesType());
            }
        }

        //Delete vehicle type page - show only
        public ActionResult Delete(int? id)
        {
            try
            {
                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                    return View(logic.GetOneVehicleTypeDetails(id));
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View(new VehiclesType());
            }
        }

        //Delete vehicle type - actual action
        [HttpPost]
        public ActionResult Delete(VehiclesType deleteType)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
                {
                    logic.DeleteVehicleType(deleteType);
                }

                return RedirectToAction("Index", "MangeHome");
            }
            catch
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                return View(new VehiclesType());
            }
        }
    }
}
