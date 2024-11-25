namespace Garage301.Models.ViewModels
{
    public class MemberDetailsViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<ParkedVehicle> Vehicles { get; set; }
    }
}
