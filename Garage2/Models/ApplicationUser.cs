using Microsoft.AspNetCore.Identity;

namespace Garage301.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<ParkedVehicle> Vehicles { get; set; }
    }
}
