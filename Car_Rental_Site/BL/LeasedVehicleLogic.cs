using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using ModelClass;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data;



namespace BL
{
    public class LeasedVehicleLogic : BaseLogic
    {
        /// <summary>
        /// Get all leased vehicles - The history.
        /// For admin.
        /// </summary>
        /// <returns>All Leased Vehicles</returns>
        public List<LeasedVehicle> GetAllVehicles()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.LeasedVehicles.ToList();
            }
        }
        /// <summary>
        /// Get all user orders by username.
        /// For users.
        /// </summary>
        /// <param name="userName">the username</param>
        /// <returns>All user orders</returns>
        public List<GetUserOrders_Result> UserOrders(string userName)
        {
            GetUserOrders_Result convert = new GetUserOrders_Result();
            var user = DB.Users.Where(u => u.UserName == userName).Select(i => i.UserID).SingleOrDefault();
            var orders = DB.LeasedVehicles.Where(u => u.UserID == user).ToList();
            List<GetUserOrders_Result> ordersForUser = new List<GetUserOrders_Result>();
            GetUserOrders_Result userOrder = new GetUserOrders_Result();
            foreach (var item in orders)
            {
                var order = convert.AddToUsersOrder(item);
                ordersForUser.Add(order);
            }
            return ordersForUser;

        }
        /// <summary>
        /// Edit orders 
        /// </summary>

        public LeasedVehicle GetOneLeasedVehicleDetails(int? carRentalID)
        {
            return DB.LeasedVehicles.Where(m => m.CarRentalID == carRentalID).SingleOrDefault();
        }

        public void EditLeasedVehicles(LeasedVehicle order)
        {
            using (ModelContext context = new ModelContext())
            {
               
                
                    context.LeasedVehicles.Add(order);
                    context.Entry(order).State = EntityState.Modified;
                    context.SaveChanges();
                    //bool saveFailed;
                    //do
                    //{
                    //    saveFailed = false;

                    //    try
                    //    {
                    //        context.SaveChanges();
                    //    }
                    //    catch (DbUpdateConcurrencyException ex)
                    //    {
                    //        saveFailed = true;

                    //        // Update the values of the entity that failed to save from the store 
                    //        //ex.Entries.Single().Reload();
                    //        var emp = ex.Entries.Single();
                    //        emp.OriginalValues.SetValues(emp.GetDatabaseValues());
                    //        DB.SaveChanges();

                    //    }

                    //} while (saveFailed); 
            }
           
        }

        /// <summary>
        /// Delete car from the orders
        /// </summary>
        /// <param name="vehicle">The details of the car to delete</param>
        public void DeleteLeasedVehicles(LeasedVehicle order)
        {
            DB.Entry(order).State = EntityState.Deleted;
            DB.SaveChanges();
        }
    }
}
