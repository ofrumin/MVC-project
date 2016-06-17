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
    [Authorize(Roles = "User,Employee,Admin")]
    public class OrderController : Controller
    {
        /// <summary>
        /// View page to order/rent vehicle - user need to confirm is order
        /// user need to supply start date, end date and vehicle type ID
        /// if the user came from the search page all those details supply automtic
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="vehicleID">The vehicle type ID</param>
        /// <returns>The view of the order to user confirm</returns>
        public ActionResult OrderPage(DateTime? startDate, DateTime? endDate, int? vehicleTypeID)
        {
            try
            {
                OrderLogic logic = new OrderLogic();

                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                return View(logic.PreOrder(startDate, endDate, vehicleTypeID));

            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.CriticalError = true;
                return View("InformationToUser");
               
                
            }
        }

        /// <summary>
        /// The actual order/rent action
        /// If the user get here he confirm is order and the order details add to the DB
        /// 
        /// all the details for this action supply automtic from the "OrderPage" view page above
        /// </summary>
        /// <param name="carNumber">The car number</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>Redirect to user history of rent</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPage(int? vehicleTypeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                OrderLogic logic = new OrderLogic();

                AccountLogic UserLogic = new AccountLogic();
                var fleetCar= logic.GetCarFromType(vehicleTypeID);

                logic.OrderingVehicle(fleetCar.CarNumber, UserLogic.GetUserID(User.Identity.Name), startDate, endDate);


                return RedirectToAction("ManageAccount", "Account");
            }
            catch (Exception)
            {
                ViewBag.Error = "We have problem on the server, Please try again later!";
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                return View("InformationToUser");
            }
        }
        
    }
}
