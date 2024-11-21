namespace Garage301.Models
{
    public class ApplicationUserVehicle
    {
        public int VehicleId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public ParkedVehicle ParkedVehicle { get; set; }
    }
}
