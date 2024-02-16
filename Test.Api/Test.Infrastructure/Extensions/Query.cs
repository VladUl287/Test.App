using System.Linq.Expressions;
using Test.Core.Dtos;
using Test.Core.Entities;

namespace Test.Infrastructure.Extensions;

internal static class Query
{
    internal static IQueryable<Employee> SetFilters(this IQueryable<Employee> query, EmployeeFilters? filters)
    {
        if (filters is null)
        {
            return query;
        }

        var employee = Expression.Parameter(typeof(Employee));
        var department = Expression.Property(employee, nameof(Employee.Department));
        var expression = employee
            .StartsWith(filters.FullName, nameof(Employee.FullName))
            .AndStartsWith(department, filters.Department, nameof(Employee.Department.Name))
            .AndEqual(employee, filters.Salary, nameof(Employee.Salary))
            .AndEqual(employee, filters.DateBirth, nameof(Employee.DateBirth))
            .AndEqual(employee, filters.DateEmployment, nameof(Employee.DateEmployment));

        if (expression is not null)
        {
            var lambda = Expression.Lambda<Func<Employee, bool>>(expression, employee);
            return query.Where(lambda);
        }

        return query;
    }
}
