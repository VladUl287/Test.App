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

        var parameter = Expression.Parameter(typeof(Employee));
        var expression = parameter
            .StartsWith(filters.FullName, nameof(Employee.FullName))
            .AndEqual(parameter, filters.Salary, nameof(Employee.Salary))
            .AndEqual(parameter, filters.DateBirth, nameof(Employee.DateBirth))
            .AndEqual(parameter, filters.DepartmentId, nameof(Employee.DepartmentId))
            .AndEqual(parameter, filters.DateEmployment, nameof(Employee.DateEmployment));

        if (expression is not null)
        {
            var lambda = Expression.Lambda<Func<Employee, bool>>(expression, parameter);
            return query.Where(lambda);
        }

        return query;
    }
}
