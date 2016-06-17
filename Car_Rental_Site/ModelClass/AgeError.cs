using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AgeValidAttribute : ValidationAttribute
    {
        public AgeValidAttribute()
        {
            ErrorMessage = "You age cannot in the range!";
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                DateTime valueAsDateTime = (DateTime)value;
                return (valueAsDateTime <= (DateTime.Today.AddYears(-18)) && valueAsDateTime >= (DateTime.Today.AddYears(-120)));
            }
            else
            {
                return true;
            }
        }
    }
}
