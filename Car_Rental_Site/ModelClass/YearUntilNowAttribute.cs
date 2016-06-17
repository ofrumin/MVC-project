using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class YearUntilNowAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validation of Date not before now
        /// </summary>
        public YearUntilNowAttribute()
        {
            ErrorMessage = "You cannot assign year late than today.";
        }

        public override bool IsValid(object value)
        {
            if (value is int)
            {
                int valueAsInt = (int)value;
                return valueAsInt <= DateTime.Now.Year;
            }
            else
            {
                return true;
            }
        }
    }
}
