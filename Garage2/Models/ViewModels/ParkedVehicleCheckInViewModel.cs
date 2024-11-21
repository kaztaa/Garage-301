using System.ComponentModel.DataAnnotations;

namespace Garage301.Models.ViewModels
{
    public class ParkedVehicleCheckInViewModel
    {
        [Display(Name = "Vehicle Type")]
        [Required]
        public int VehicleTypesId { get; set; }

        [Display(Name = "Registration Number")]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3}[0-9]{3}$", ErrorMessage = "Registration number must have three letters followed by three digits (e.g., ABC123).")]
        public string RegistrationNumber { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 2, ErrorMessage = "Make must be between 2 and 12 characters.")]
        public string Make { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 2, ErrorMessage = "Model must be between 2 and 12 characters.")]
        public string Model { get; set; }

        [Display(Name = "Number of Wheels")]
        [Range(1, 10, ErrorMessage = "Number of wheels must be at least 1 and at most 10.")]
        public int NumberOfWheels { get; set; }

        [Display(Name = "Parking Spot")]
        [Required]
        public int ParkingSpotId { get; set; }
    }
}
