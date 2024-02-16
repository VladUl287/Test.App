using Microsoft.EntityFrameworkCore;
using Test.Core.Contracts.Repositories;
using Test.Core.Entities;
using Test.Infrastructure.Database;

namespace Test.Infrastructure.Repositories;

public sealed class EmployeeManager : IEmployeeManager
{
    private readonly DatabaseContext dbContext;

    public EmployeeManager(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Create(Employee employee)
    {
        await dbContext.Employees.AddAsync(employee);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(Employee employee)
    {
        dbContext.Employees.Update(employee);
        await dbContext.SaveChangesAsync();
    }
}
