using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Rental_Site.Models
{
    public class UserRoleVM
    {
        public string RoleName { get; set; }
        public bool AssignedToRole { get; set; }
    }
}