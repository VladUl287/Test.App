using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Infrastructure.Database.Configuration;

namespace Test.Infrastructure.Database;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {}

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfig());
        modelBuilder.ApplyConfiguration(new DepartmentConfig());

        modelBuilder.Entity<Department>()
            .HasData(new Department[]
            {
                new() {
                    Id = 1,
                    Name = "Здравоохранение"
                },
                new() {
                    Id = 2,
                    Name = "Экономическое развитие"
                }
            });

        base.OnModelCreating(modelBuilder);
    }
}
