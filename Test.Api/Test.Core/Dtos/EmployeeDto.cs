namespace Test.Core.Dtos;

public sealed class EmployeeDto
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = string.Empty;

    public DateTime DateBirth { get; init; }

    public DateTime DateEmployment { get; init; }

    public decimal Salary { get; init; }

    public DepartmentDto? Department { get; init; }
}
