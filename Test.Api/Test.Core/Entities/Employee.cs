namespace Test.Core.Entities;

public sealed class Employee
{
    public Guid Id { get; init; }

    public string FullName { get; set; } = string.Empty;

    public DateTime DateBirth { get; set; }

    public DateTime DateEmployment { get; set; }

    public decimal Salary { get; set; }

    public long DepartmentId { get; set; }

    public Department Department { get; set; } = default!;
}
