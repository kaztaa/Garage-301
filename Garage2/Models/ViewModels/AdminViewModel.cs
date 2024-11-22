namespace Garage301.Models.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<ParkingSpot> ParkingSpots { get; set; }
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }
    }
}
