using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Appointment.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="The {0} must be at least {2} caracters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [DisplayName("Confirm Passowrd")]
        [Compare("Password", ErrorMessage = "The passwords don't macth")]
        public string ConfirmPassowrd { get; set; }

        [Required]
        [DisplayName("Role name")]
        public string RoleName { get; set; }

    }
}
