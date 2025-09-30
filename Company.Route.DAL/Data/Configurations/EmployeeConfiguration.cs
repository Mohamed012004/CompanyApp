using Company.Route.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Route.DAL.Data.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");



            builder.HasOne(E => E.Department)
                    .WithMany(D => D.Employees)
                    .HasForeignKey(E => E.DepartmentID)
                    .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
