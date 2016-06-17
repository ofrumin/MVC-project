using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;
using System.Threading;
using DAL;

namespace BL
{
    public class OrderLogic : BaseLogic
    {
        /// <summary>
        /// See the details of the car the user want to order and check again if is free between two dates.
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="vehicleID">The Type ID</param>
        /// <returns>The vehicle type to show the user</returns>
        public VehiclesType PreOrder(DateTime? startDate, DateTime? endDate, int? vehicleTypeID)
        {
            try
            {
                var type = DB.VehiclesFleets.Where(v => v.Type.VehicleTypeID == vehicleTypeID).Select(s => s.Type).FirstOrDefault();
                return type;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// After the user confirms is order this enter it into the DB.
        /// </summary>
        /// <param name="carNum">The car number</param>
        /// <param name="userID">the user ID who create this order</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        public void OrderingVehicle(string carNum, int userID, DateTime startDate, DateTime endDate)
        {
            var vehichleID = DB.VehiclesFleets.Where(v => v.CarNumber == carNum).FirstOrDefault();

            var leasedVehicleToLeaseAfter = DB.LeasedVehicles.Where(l => l.CarNumber == carNum && (l.SoughtReturnDate >= startDate && l.RentalStartDate <= endDate)).FirstOrDefault();
            var leasedVehicleToLeaseBefore = DB.LeasedVehicles.Where(l => l.CarNumber == carNum && (l.RentalStartDate <= startDate && l.SoughtReturnDate >= endDate)).FirstOrDefault();
            var leasedVehicleToLeaseExternal = DB.LeasedVehicles.Where(l => l.CarNumber == carNum && (l.RentalStartDate >= startDate && l.SoughtReturnDate <= endDate)).FirstOrDefault();

            if (leasedVehicleToLeaseAfter != null || leasedVehicleToLeaseBefore != null || leasedVehicleToLeaseExternal != null)
            {
                throw new Exception();
            }
            else
            {
                LeasedVehicle leasedVehicleToAdd = new LeasedVehicle
                {
                    CarNumber = carNum,
                    UserID = userID,
                    RentalStartDate = startDate,
                    SoughtReturnDate = endDate,
                    VehiclesFleet = vehichleID,

                };

                DB.LeasedVehicles.Attach(leasedVehicleToAdd);
                DB.Entry(leasedVehicleToAdd).State = EntityState.Added;
                DB.SaveChanges();
            }

        }
        public VehiclesFleet GetCarFromType(int? vehicleTypeID)
        {
            var vehichle = DB.VehiclesFleets.Where(v => v.TypeID == vehicleTypeID).FirstOrDefault();
            return vehichle;
        }
    }
}
