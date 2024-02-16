namespace Test.Core.Dtos;

public sealed class EmployeeFilters
{
    public string? FullName { get; init; } = string.Empty;

    public decimal? Salary { get; init; }

    public DateTime? DateBirth { get; init; }

    public DateTime? DateEmployment { get; init; }

    public long? DepartmentId { get; init; }
}
