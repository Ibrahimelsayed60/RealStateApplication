using Microsoft.EntityFrameworkCore;
using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Infrastructure.Data
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Villa> Viilas { get; set; }

        public DbSet<VillaNumber> VillaNumbers { get; set; }
    }
}
