using System.ComponentModel.DataAnnotations;

namespace soundyard.club.Models
{
    public class UserForLogin
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } 
    }
}
