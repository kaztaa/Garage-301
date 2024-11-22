using System.ComponentModel.DataAnnotations;

namespace Garage301.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required]
        public int VehicleTypesId { get; set; } // Foreign key to VehicleTypes

        public VehicleTypes VehicleType { get; set; } // Navigation property

        [Display(Name = "Registration number")]
        [Required]
        // Make sure regnr format is correct, first three should be letters and last three should be numbers
        [RegularExpression(@"^[A-Za-z]{3}[0-9]{3}$", ErrorMessage = "Registration number must have three letters followed by three digits (e.g., ABC123).")]
        public string RegistrationNumber { get; set; }

        [Required]
        public string Color { get; set; }

        [StringLength(12, MinimumLength = 2, ErrorMessage = "Make must be between 2 and 12 characters.")]
        [Required]
        public string Make { get; set; }

        [StringLength(12, MinimumLength = 2, ErrorMessage = "Model must be between 2 and 12 characters.")]
        [Required]
        public string Model { get; set; }

        [Display(Name = "Number of wheels")]
        [Range(1, 10, ErrorMessage = "Number of wheels must be at least 1 and at most 10.")]
        public int NumberOfWheels { get; set; }

        public int? ParkingSpotId { get; set; } // Foreign key to ParkingSpot

        public ParkingSpot? ParkingSpot { get; set; } // Navigation property

        [Display(Name = "Arrival time")]
        public DateTime ArrivalTime { get; set; } = DateTime.Now;

        public DateTime? CheckoutTime { get; set; }

        [Display(Name = "Parked duration")]
        public string ParkedDuration
        {
            get
            {
                var duration = DateTime.Now - ArrivalTime;
                return $"{duration.Hours} Hours, {duration.Minutes} minutes";
            }
        }

        [Display(Name = "Price per hour")]
        private const decimal PricePerHour = 10;

        [Display(Name = "Parking cost")]
        public decimal ParkingCost
        {
            get
            {
                if (CheckoutTime == null)
                {
                    var parkingDuration = DateTime.Now - ArrivalTime;
                    return (decimal)Math.Ceiling(parkingDuration.TotalHours) * PricePerHour;
                }
                else
                {
                    var parkingDuration = CheckoutTime.Value - ArrivalTime;
                    return (decimal)Math.Ceiling(parkingDuration.TotalHours) * PricePerHour;
                }
            }
        }

        // Foreign Key to ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; } // Navigation property back to user
    }
}
