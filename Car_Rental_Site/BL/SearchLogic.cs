using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;
using System.Threading;


namespace BL
{
    public class SearchLogic : BaseLogic
    {
        /// <summary>
        /// Search Vehicles type to rent between two dates.
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>List of available vehicle type between the two dates</returns>

        public List<VehiclesType> SearchByDates(DateTime? startDate, DateTime? endDate)
        {
            
            var availableTypsFromFleet = DB.VehiclesFleets.Select(sel => sel.Type).Distinct().ToList();

            return availableTypsFromFleet;

        }

    }
}
