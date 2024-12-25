using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace club.soundyard.web.Models
{
    public class AppDbContext : DbContext
    {



        public AppDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<User> Users { get; set; }
    }
}