using Test.Core.Dtos;
using Test.Core.Entities;

namespace Test.Core.Contracts.Repositories;

public interface IEmployeeManager
{
    Task Create(Employee employee);

    Task Update(Employee employee);

    Task Delete(Guid id);
}
