using Company.Route.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Company.Route.DAL.Data.Contexts
{
    internal class CompanyDbContext : DbContext
    {
        public CompanyDbContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=.; DataBase=CompanyApp; Trusted_Connection=True; TrustServerCertificate=True");

        }

        public DbSet<Department> Departments { get; set; }
    }
}
