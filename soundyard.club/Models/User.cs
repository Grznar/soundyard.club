using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace club.soundyard.web.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
        public string Password {  get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Role { get; set; }



        [MaxLength(50)]
        public string Agreement { get; set; }
    }
}