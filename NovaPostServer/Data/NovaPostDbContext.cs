using Microsoft.EntityFrameworkCore;
using NovaPostServer.Constants;
using NovaPostServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Data
{
    public class NovaPostDbContext : DbContext
    {
        public DbSet<AreaEntity> Areas { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(AppDatabase.ConnectionString);
        }
    }
}
