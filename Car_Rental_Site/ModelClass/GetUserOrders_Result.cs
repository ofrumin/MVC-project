using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ModelClass
{
    /// <summary>
    /// Get to User recent orders
    /// </summary>
    public partial class GetUserOrders_Result
    {
        public string CarNamber { get; set; }
        public string RentalStartDate { get; set; }
        public string SoughtReturnDate { get; set; }
        public string ActualReturnDate { get; set; }

        public GetUserOrders_Result AddToUsersOrder(LeasedVehicle leasedVehicle)
        {
            GetUserOrders_Result ordersForUser = new GetUserOrders_Result();
            ordersForUser.CarNamber=leasedVehicle.CarNumber;
            ordersForUser.RentalStartDate = leasedVehicle.RentalStartDate.ToShortDateString();
            ordersForUser.SoughtReturnDate = leasedVehicle.SoughtReturnDate.ToShortDateString();
            ordersForUser.ActualReturnDate = leasedVehicle.ActualReturnDate.ToString() ;
            return ordersForUser;

        }

    }
}
