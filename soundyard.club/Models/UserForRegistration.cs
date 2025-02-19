﻿using System.ComponentModel.DataAnnotations;

namespace club.soundyard.web.Models
{
    public class UserForRegistration
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } 

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } 
    }
}
