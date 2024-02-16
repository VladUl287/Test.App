using Mapster;
using Test.Core.Dtos;
using Test.Core.Entities;
using Test.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Test.Core.Contracts.Repositories;
using Test.Infrastructure.Extensions;

namespace Test.Infrastructure.Repositories;

public sealed class EmployeePresenter : IEmployeePresenter
{
    private readonly DatabaseContext dbContext;

    public EmployeePresenter(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<bool> Exists(Guid id)
    {
        return dbContext.Employees.AnyAsync(e => e.Id == id);
    }

    public Task<Employee?> Get(Guid id)
    {
        return dbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<EmployeeDto[]> GetAll(EmployeeFilters? filters)
    {
        return dbContext.Employees
            .SetFilters(filters)
            .ProjectToType<EmployeeDto>()
            .ToArrayAsync();
    }
}
