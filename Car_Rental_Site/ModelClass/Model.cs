using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Model
{
    public partial class Branch
    {
        public Branch()
        {
            this.VehiclesFleets = new HashSet<VehiclesFleet>();
        }
        [Key]
        public int BranchID { get; set; }
        [Required, StringLength(100), Index(IsUnique = true)]
        public string BranchName { get; set; }
        [Required(ErrorMessage = "You must enter the branch address!")]
        public string Address { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }

        public ICollection<VehiclesFleet> VehiclesFleets { get; set; }
    }
    public partial class LeasedVehicle
    {
        [Key]
        public int CarRentalID { get; set; }
        public string CarNumber { get; set; }
        public int UserID { get; set; }
        [DataType(DataType.Date)]
        //,RegularExpression("^([0-2]{0,1}[0-9]{1}|3[0-1])[/,.]{1}(0{0,1}[1-9]{1}|(1[0-2]){1})[/,.]{1}20[0-2]{1}[0-9]{1}$", ErrorMessage = "Date most by in MM/dd/yyyy format!")]
        public System.DateTime RentalStartDate { get; set; }
        [DataType(DataType.Date)]
        //[RegularExpression("^([0-2]{0,1}[0-9]{1}|3[0-1])[/,.]{1}(0{0,1}[1-9]{1}|(1[0-2]){1})[/,.]{1}20[0-2]{1}[0-9]{1}$", ErrorMessage = "Date most by in MM/dd/yyyy format!")]
        public System.DateTime SoughtReturnDate { get; set; }
        [DataType(DataType.Date)]
        //[RegularExpression("^([0-2]{0,1}[0-9]{1}|3[0-1])[/,.]{1}(0{0,1}[1-9]{1}|(1[0-2]){1})[/,.]{1}20[0-2]{1}[0-9]{1}$", ErrorMessage = "Date most by in MM/dd/yyyy format!")]
        public Nullable<System.DateTime> ActualReturnDate { get; set; }

        public User User { get; set; }
        public VehiclesFleet VehiclesFleet { get; set; }
    }
    public partial class User
    {
        public User()
        {
            this.LeasedVehicles = new HashSet<LeasedVehicle>();
            Roles = new HashSet<Role>();
        }
        [Key]
        public int UserID { get; set; }

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
        public string UserName { get; set; }

        [NotMapped]
        [MaxLength(50, ErrorMessage = "Password can't contain more then 50 letters!")]
        [MinLength(4, ErrorMessage = "Password most contain at least 4 letters!")]
        public string Password { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "Password can't contain more then 50 letters!")]
        [MinLength(4, ErrorMessage = "Password most contain at least 4 letters!")]
        public string HashedPassword { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, AgeValidAttribute]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public ICollection<LeasedVehicle> LeasedVehicles { get; set; }

    }

    public partial class VehiclesFleet
    {
        public VehiclesFleet()
        {
            this.LeasedVehicles = new HashSet<LeasedVehicle>();
        }
        [Key]
        public int VehicleID { get; set; }
        [Required(ErrorMessage = "You must enter car number!")]
        [RegularExpression(@"^[1-9]{1}[0-9]-[0-9]{3}-[0-9]{2}$", ErrorMessage = "Care number most by in nn-nnn-nn format!")]
        public string CarNumber { get; set; }

        [Required]
        public int CurrentMileage { get; set; }

        [Required(ErrorMessage = "You must enter picture string/link!")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "Are this car in proper condition!")]
        public bool Proper { get; set; }

        [Required(ErrorMessage = "You must enter branch number!")]
        public int BranchID { get; set; }

        [Required]
        public int TypeID { get; set; }

        public virtual VehiclesType Type { get; set; }
        public virtual Branch Branch { get; set; }
        public ICollection<LeasedVehicle> LeasedVehicles { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (VehicleID < 1)
            {
                errors.Add(new ValidationResult("Vehicle ID most be biger then 0!", new List<string>() { "VehicleID" }));
            }

            if (CurrentMileage < 0)
            {
                errors.Add(new ValidationResult("Current Mileage most be biger then 0!", new List<string>() { "CurrentMileage" }));
            }

            return errors;
        }
    }
    public partial class VehiclesType
    {
        
        [Key]
        public int VehicleTypeID { get; set; }
        [RegularExpression("[A-Za-z].*", ErrorMessage = "Manufacturer must start with letter")]
        [MaxLength(25, ErrorMessage = "Model name can't contain more then 25 letters!")]
        [Required(ErrorMessage = "You must enter Manufacturer name!")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "You must enter model name!")]
        public string Model { get; set; }
        [Range(2000, double.MaxValue), YearUntilNow]
        public int ManufactureYear { get; set; }
        public string Transmission { get; set; }
        [Required(ErrorMessage = "You must enter cost per day!")]
        public decimal DailyRentCost { get; set; }
        [Required(ErrorMessage = "You must enter delay cost per day!")]
        public decimal RentCostForDelay { get; set; }
        [Required(ErrorMessage = "You must enter picture string/link!")]
        [MaxLength(150, ErrorMessage = "Picture string/link can't contain more then 150 letters!")]
        public string Picture { get; set; }
        public ICollection<VehiclesFleet> VehiclesFleets { get; set; }
        public VehiclesType()
        {
            this.VehiclesFleets = new HashSet<VehiclesFleet>();
        }
    }
    public partial class Role
    {
        public const int MAX_ROLENAME_LENGTH = 100;
        [Key]
        public int RoleID { get; set; }
        [Required, StringLength(MAX_ROLENAME_LENGTH), Index(IsUnique = true)]
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
