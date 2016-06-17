using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model;
using BL;


namespace Car_Rental_Site.Models
{
    public class UserRegistrationVM : IValidatableObject
    {
        [Required]
        [MinLength(3, ErrorMessage = "First name most contain at least 3 letters!")]
        [MaxLength(15, ErrorMessage = "First name can't contain more then 15 letters!")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Last name most contain at least 3 letters!")]
        [MaxLength(20, ErrorMessage = "Last name can't contain more then 20 letters!")]
        public string LastName { get; set; }

        [Required]
        [MinLength(9, ErrorMessage = "Identity most contain at least 9 letters!")]
        [MaxLength(9, ErrorMessage = "Identity can't contain more then 9 letters!")]
        public string ID { get; set; }

        [Required, Index(IsUnique = true)] 
        [MinLength(2, ErrorMessage = "User name most contain at least 2 letters!")]
        [MaxLength(10, ErrorMessage = "User name can't contain more then 10 letters!")]
        [CheckUsername]
        public string Username { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Password can't contain more then 50 letters!")]
        [MinLength(4, ErrorMessage = "Password most contain at least 4 letters!")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, AgeValidAttribute]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Password != ConfirmPassword)
            {
                errors.Add(new ValidationResult("The supplied passwords do not match"));
            }
            return errors;
        }

       

        public User GetUser()
        {
            return new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                ID = ID,
                UserName = Username,
                Password = Password,
                Email = Email,
                BirthDate = BirthDate,
                Gender = Gender
            };
        }

    }
}

