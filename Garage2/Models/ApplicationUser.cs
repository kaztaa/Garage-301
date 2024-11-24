using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Garage301.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [CustomValidation(typeof(ApplicationUser), nameof(ValidateFirstAndLastName))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<ParkedVehicle> Vehicles { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}\d{4}$", ErrorMessage = "Personnummer must be in the format yyMMddxxxx (e.g., 9701231234).")]
        public string Personnummer { get; set; } = string.Empty;

        public static ValidationResult ValidateFirstAndLastName(object value, ValidationContext context)
        {
            var user = (ApplicationUser)context.ObjectInstance;
            if (user.FirstName.Equals(user.LastName, StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("First name cannot be the same as last name.");
            }
            return ValidationResult.Success!;
        }
    }
}
