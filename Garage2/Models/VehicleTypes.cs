using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Garage301.Models
{
    public class VehicleTypes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Vehicle type name must be between 2 and 50 characters.")]
        public string Name { get; set; } = string.Empty;

        // Navigation property for ParkedVehicles
        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}
