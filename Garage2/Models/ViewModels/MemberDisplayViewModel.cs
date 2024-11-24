namespace Garage301.Models.ViewModels
{
    public class MemberDisplayViewModel
    {
        public int VehicleCount { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
