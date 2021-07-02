using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstTask.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FirstTask.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser, CustomRole, Guid>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<CustomUser> CustomUsers { get; set; }
        public DbSet<CustomRole> CustomRoles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }
    }
}

