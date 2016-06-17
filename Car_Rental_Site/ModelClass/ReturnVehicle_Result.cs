using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    /// <summary>
    /// For calculate final cost
    /// </summary>
    public class ReturnVehicle_Result
    {
        public ReturnVehicle_Result()
        {
            TotalDays = 0;
            TotalRegularDays = 0;
            TotalDelayDays = 0;
            PaymentForRegularDays = 0;
            PaymentForDelay = 0;
        }
        public Nullable<int> TotalDays { get; set; }
        public Nullable<int> TotalRegularDays { get; set; }
        public Nullable<int> TotalDelayDays { get; set; }
        public decimal PaymentForRegularDays { get; set; }
        public decimal PaymentForDelay { get; set; }
    }
}
