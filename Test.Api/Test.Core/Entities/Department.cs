namespace Test.Core.Entities;

public sealed class Department
{
    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public IEnumerable<Employee> Employees { get; init; } = Enumerable.Empty<Employee>();
}
