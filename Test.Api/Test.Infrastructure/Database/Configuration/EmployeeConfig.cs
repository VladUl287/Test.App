using Test.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Test.Infrastructure.Database.Configuration;

internal sealed class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FullName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Salary)
            .IsRequired();

        builder.Property(e => e.DateBirth)
            .IsRequired();

        builder.Property(e => e.DateEmployment)
            .IsRequired();   
        
        builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);
    }
}
