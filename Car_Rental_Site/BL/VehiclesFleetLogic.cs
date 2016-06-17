using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace BL
{
    public class VehiclesFleetLogic : BaseLogic
    {
        /// <summary>
        /// Get the details of all the vehicles in the fleet
        /// </summary>
        /// <returns>list of vehicle</returns>
        public List<VehiclesFleet> GetAllVehiclesFleet()
        {
            return DB.VehiclesFleets.ToList();
        }

        /// <summary>
        /// Get one car details, to edit/delete
        /// </summary>
        /// <param name="carNumber">The car number</param>
        /// <returns>the car details</returns>
        public VehiclesFleet GetOneCarDetails(String carNumber)
        {
            return DB.VehiclesFleets.Where(vehicle => vehicle.CarNumber == carNumber).FirstOrDefault();
        }

        /// <summary>
        /// Add new car to the fleet
        /// </summary>
        /// <param name="vehicle">the new car details</param>
        public void AddVehiclesToFleet(VehiclesFleet vehicle)
        {
            DB.VehiclesTypes.Include(t => t.VehiclesFleets);
            DB.VehiclesFleets.Include(v => v.LeasedVehicles);
            DB.VehiclesFleets.Add(vehicle);
            DB.SaveChanges();
        }

        /// <summary>
        /// Edit car from the fleet
        /// </summary>
        /// <param name="vehicle">the car details with the updates</param>
        public void EditVehiclesFromFleet(VehiclesFleet vehicle)
        {
            DB.VehiclesFleets.Add(vehicle);
            DB.Entry(vehicle).State = EntityState.Modified;
            DB.SaveChanges();
        }

        /// <summary>
        /// Delete car from the fleet
        /// </summary>
        /// <param name="vehicle">The details of the car to delete</param>
        public void DeleteVehiclesFromFleet(VehiclesFleet vehicle)
        {
            try
            {
                var IfExist = DB.LeasedVehicles.Where(v => v.CarNumber == vehicle.CarNumber).FirstOrDefault();
                if (IfExist == null)
                {
                    DB.Entry(vehicle).State = EntityState.Deleted;
                    DB.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Checking if vehicle number exists
        /// </summary>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public bool CheckVehicleNumber(string carNumber)
        {
            var IsExists = DB.VehiclesFleets.Where(u => u.CarNumber == carNumber).FirstOrDefault();
            if (IsExists != null)
            {
                return false;
            }
            return true;
        }
    }
}
