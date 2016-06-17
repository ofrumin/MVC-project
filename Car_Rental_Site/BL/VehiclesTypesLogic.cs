using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace BL
{
    public class VehiclesTypesLogic : BaseLogic
    {
         /// <summary>
        /// Get all Vehicle types
        /// </summary>
        /// <returns></returns>
        public List<VehiclesType> GetAllVehiclesTypes()
        {
           return DB.VehiclesTypes.ToList();
        }

        /// <summary>
        /// Get the details of one vehicle type
        /// </summary>
        /// <param name="vehicleCode">the type ID</param>
        /// <returns>the details of the vehicle</returns>
        public VehiclesType GetOneVehicleTypeDetails(int? VehicleTypeID)
        {
            return DB.VehiclesTypes.Where(m => m.VehicleTypeID == VehicleTypeID).SingleOrDefault();
        }

        /// <summary>
        /// Add new vehicle type
        /// </summary>
        /// <param name="Type">The new type details</param>
        public void AddVehicleType(VehiclesType Type)
        {
            DB.VehiclesTypes.Add(Type);
            DB.SaveChanges();
        }

        /// <summary>
        /// Edit exist vehicle type
        /// </summary>
        /// <param name="Type">The vehicle type details with the updates</param>
        public void EditVehicleType(VehiclesType Type)
        {
            DB.VehiclesTypes.Add(Type);
            DB.Entry(Type).State = EntityState.Modified;
            DB.SaveChanges();
        }

        /// <summary>
        /// Delete vehicle type
        /// </summary>
        /// <param name="Type">The details of the type to delete</param>
        public void DeleteVehicleType(VehiclesType Type)
        {
            DB.Entry(Type).State = EntityState.Deleted;
            DB.SaveChanges();
        }

        /// <summary>
        /// Get list of all manufacturers in the DB
        /// </summary>
        /// <returns>list of all manufacturers</returns>
        public List<string> listOfManufacturer()
        {
            var query = (from v in DB.VehiclesTypes
                         select v.Manufacturer).ToList();
            return query;
                    
        }

        /// <summary>
        /// Get list of all models in the DB
        /// </summary>
        /// <returns>list of all models</returns>
        public List<string> listOfModels()
        {
            var query = (from v in DB.VehiclesTypes
                         select v.Model).ToList();
            return query;
            
        }

        /// <summary>
        /// Get list of all Manufacture years from the DB
        /// </summary>
        /// <returns>list of all manufacture years</returns>
        public List<int> listOfYears()
        {
            var query = (from v in DB.VehiclesTypes
                         select v.ManufactureYear).ToList();
            return query;
           
        }
    }
    
}
