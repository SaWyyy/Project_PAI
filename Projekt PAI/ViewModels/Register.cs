using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projekt_PAI.ViewModels
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Hasła się nie zgadzają")]
        public string ConfirmPassword { get; set; }
    }
}
