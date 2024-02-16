using Microsoft.EntityFrameworkCore;
using Test.Core.Contracts.Repositories;
using Test.Infrastructure.Database;

namespace Test.Infrastructure.Repositories;

public sealed class DepartmentPresenter : IDepartmentPresenter
{
    private readonly DatabaseContext dbContext;

    public DepartmentPresenter(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<bool> Exists(long id)
    {
        return dbContext.Departments.AnyAsync(d => d.Id == id);
    }
}
