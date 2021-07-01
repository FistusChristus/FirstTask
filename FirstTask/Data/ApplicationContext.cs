using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstTask.Models.DbModels;

namespace FirstTask.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }
    }
}

