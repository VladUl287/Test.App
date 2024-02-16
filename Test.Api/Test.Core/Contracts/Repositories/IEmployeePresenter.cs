using Test.Core.Dtos;
using Test.Core.Entities;

namespace Test.Core.Contracts.Repositories;

public interface IEmployeePresenter
{
    Task<bool> Exists(Guid id);

    Task<Employee?> Get(Guid id);

    Task<EmployeeDto[]> GetAll(EmployeeFilters? filters);
}
