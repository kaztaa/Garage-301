namespace Garage301.Models.ViewModels
{
    public class RoleIndexViewModel
    {
        // this viewmodel is created to display list of editable/non-editable vehicles
        public string CurrentUserId { get; set; }
        public List<ParkedVehicle> Vehicles { get; set; }
        public List<ParkingSpot> ParkingSpots { get; set; }
        public string SearchField { get; set; }
        public string CurrentFilter { get; set; }
        public int CurrentType { get; set; }
        public string SortBy { get; set; }
    }
}
