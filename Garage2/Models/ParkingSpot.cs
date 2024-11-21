using System.ComponentModel.DataAnnotations;

namespace Garage301.Models
{
    public class ParkingSpot
    {
        public int Id { get; set; }

        [Display(Name = "Spot Number")]
        public int SpotNumber { get; set; }

        [Display(Name = "Occupied")]
        public bool IsOccupied { get; set; } = false;

        [Display(Name = "Assigned Vehicle")]
        public int? ParkedVehicleId { get; set; }

        // Relationship to ParkedVehicle
        public ParkedVehicle? ParkedVehicle { get; set; }

        [Display(Name = "Location")]
        [StringLength(50, ErrorMessage = "Location description should be up to 50 characters.")]
        public string? Location { get; set; }

        // Enforce a maximum of 12 spots
        public const int MaxParkingSpots = 12;
    }
}
