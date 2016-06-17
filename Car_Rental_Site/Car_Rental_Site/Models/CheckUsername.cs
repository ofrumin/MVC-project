using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BL;


namespace Car_Rental_Site.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckUsernameAttribute : ValidationAttribute
    {
        public CheckUsernameAttribute()
        {
            ErrorMessage = "The username already exists. Try another name!";
        }

        public override bool IsValid(object value)
        {
            if (value is string)
            {
                string valueAsString = (string)value;
                UsersLogic logic = new UsersLogic();
                return (logic.CheckUsername(valueAsString));
            }
            else
            {
                return true;
            }
        }
    }
}
