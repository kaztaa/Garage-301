using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Garage301.Models.ViewModels
{
    public class EditParkedVehicleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle type")]
        public int VehicleTypesId { get; set; } // Selected vehicle type ID
        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>(); // Available vehicle types

        [Display(Name = "Registration number")]
        [RegularExpression(@"^[A-Za-z]{3}[0-9]{3}$", ErrorMessage = "Registration number must have three letters followed by three digits (e.g., ABC123).")]
        public string RegistrationNumber { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        [StringLength(12, MinimumLength = 2, ErrorMessage = "Make must be between 2 and 12 characters.")]
        public string Make { get; set; } = string.Empty;

        [StringLength(12, MinimumLength = 2, ErrorMessage = "Model must be between 2 and 12 characters.")]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "Number of wheels")]
        [Range(1, 10, ErrorMessage = "Number of wheels must be at least 1 and at most 10.")]
        public int NumberOfWheels { get; set; }

        [Display(Name = "Arrival time")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Parking spot")]
        public int? ParkingSpotId { get; set; } // Selected parking spot ID, nullable if not assigned
        public IEnumerable<SelectListItem> ParkingSpots { get; set; } = new List<SelectListItem>(); // Available parking spots
    }
}