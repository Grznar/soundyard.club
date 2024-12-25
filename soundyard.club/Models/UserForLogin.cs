using System.ComponentModel.DataAnnotations;

namespace club.soundyard.web.Models
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
