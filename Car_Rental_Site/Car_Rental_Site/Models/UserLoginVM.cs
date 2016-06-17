using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Site.Models
{
    public class UserLoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool PersistentLogin { get; set; }
    }
}