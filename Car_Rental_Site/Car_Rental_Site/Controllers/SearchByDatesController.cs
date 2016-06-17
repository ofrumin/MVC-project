using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using Model;
using System.Net;




namespace Car_Rental_Site.Controllers
{
    public class SearchByDatesController : Controller
    {
        /// <summary>
        /// Search vehicles for rent - view page
        /// In this page the user can search vehicles for rent by to dates and filter the results by JS - jquery
        /// 
        /// For the filter this action connects to the DB - if he can't connect "CriticalError" rising and the user redirect to the home page
        /// In this way the user can't start search if the server has problems.
        /// </summary>
        /// <returns>The search vehicles page</returns>
        public ActionResult Search()
        {
            try
            {
            using (VehiclesTypesLogic logic = new VehiclesTypesLogic())
            {
                ViewBag.ManufacturersList = logic.listOfManufacturer();
                ViewBag.ModelsList = logic.listOfModels();
                ViewBag.YearsList = logic.listOfYears();
                return View();
            }
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                ViewBag.ManufacturersList = new List<string>();
                ViewBag.ModelsList = new List<string>();
                ViewBag.YearsList = new List<int>();
                return View(new VehiclesType());
            }
        }

        /// <summary>
        /// Return all vehicles that available for rent between two dates
        /// All the logic to know if the vehicle available or not supplied in the BL 
        /// 
        /// After this The filter menu displayed
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>To ajax list of vehicle types / BadRequest</returns>
        public ActionResult SearchResults(DateTime? startDate, DateTime? endDate)
        {
            try
            {
            SearchLogic logic = new SearchLogic();
            return Json(logic.SearchByDates(startDate.Value, endDate.Value), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "We have problem on the server, Please try again later!");
            }
        }

    }
}
