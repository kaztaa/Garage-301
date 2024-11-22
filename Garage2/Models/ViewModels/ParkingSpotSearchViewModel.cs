namespace Garage301.Models.ViewModels
{
    public class ParkingSpotSearchViewModel
    {
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } = "RegistrationNumber"; // Default sorting option
        public string SortDirection { get; set; } = "asc"; // Default sort direction
        public List<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>(); // The list of parked vehicles
    }
}
