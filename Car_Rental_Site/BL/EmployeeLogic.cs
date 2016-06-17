using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ComponentModel.DataAnnotations;
using ModelClass;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;
using System.Threading;


namespace BL
{
    public class EmployeeLogic : BaseLogic
    {
        /// <summary>
        /// Get list of all vehicles that need to return
        /// </summary>
        /// <returns>list of vehicles that need to return</returns>
        public List<LeasedVehicle> LeasededVehicles()
        {
            List<User> users = DB.Users.ToList();
            List<VehiclesFleet> fleet = DB.VehiclesFleets.ToList();
            List<LeasedVehicle> leasededVehiclesToReturn = new List<LeasedVehicle>();
            var allVehiclesToReturn = DB.LeasedVehicles.ToList();
            return allVehiclesToReturn;
        }

        /// <summary>
        /// Return the vehicle and return the price and days number.
        /// Calculate the price the user need to pay by those details
        /// </summary>
        /// <param name="rentalID">the rental ID</param>
        /// <returns>Number of days and how much to pay</returns>

        public ReturnVehicle_Result ReturnVehicle(int? rentalID)
        {
            ReturnVehicle_Result calcOfPrice=new ReturnVehicle_Result();
            var query = (from v in DB.LeasedVehicles
                         where v.CarRentalID == rentalID
                         select v).FirstOrDefault();

            var priceDailyRent = DB.VehiclesFleets.
                Where(n=>n.CarNumber==query.CarNumber).FirstOrDefault().Type.DailyRentCost;
            var priceRentCostForDelay = DB.VehiclesFleets.
             Where(n => n.CarNumber == query.CarNumber).FirstOrDefault().Type.RentCostForDelay;         

            calcOfPrice.TotalRegularDays = (int)query.SoughtReturnDate.Subtract(query.RentalStartDate).TotalDays;
            
                    
                if (query.ActualReturnDate != null)
                {
                    DateTime actualReturnDate = query.ActualReturnDate.Value;
                    calcOfPrice.TotalDelayDays = (int)actualReturnDate.Subtract(query.SoughtReturnDate).TotalDays;
                }
                calcOfPrice.TotalDays = (int)calcOfPrice.TotalDelayDays + (int)calcOfPrice.TotalRegularDays;
                #region CalculateThePrice
                if (calcOfPrice.TotalDelayDays < 0)
                {
                    calcOfPrice.PaymentForDelay = 0;
                    calcOfPrice.PaymentForRegularDays = (decimal)calcOfPrice.TotalDays * priceDailyRent;
                }
                if (calcOfPrice.TotalDays <= 0)
                {
                    calcOfPrice.PaymentForDelay = 0;
                    calcOfPrice.PaymentForRegularDays = 0;
                }
                if (calcOfPrice.TotalDays == calcOfPrice.TotalRegularDays)
                {
                    calcOfPrice.PaymentForDelay = 0;
                    calcOfPrice.PaymentForRegularDays = (decimal)calcOfPrice.TotalDays * priceRentCostForDelay;
                }
                if (calcOfPrice.TotalDays > calcOfPrice.TotalRegularDays)
                {
                    calcOfPrice.PaymentForDelay = (decimal)(calcOfPrice.TotalDays - calcOfPrice.TotalRegularDays) * priceRentCostForDelay;
                    calcOfPrice.PaymentForRegularDays = (decimal)calcOfPrice.TotalRegularDays * priceDailyRent;
                }
                #endregion
                return calcOfPrice;
        }
    }

}
