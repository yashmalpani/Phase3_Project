using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<Laptop> Laptops{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuring Connection String
            optionsBuilder.UseSqlServer("Server=.;Database=FinalProj;Integrated Security=true;");
        }
    }
}
