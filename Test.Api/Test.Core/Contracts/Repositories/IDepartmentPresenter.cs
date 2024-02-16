using Test.Core.Dtos;

namespace Test.Core.Contracts.Repositories;

public interface IDepartmentPresenter
{
    Task<bool> Exists(long id);

    Task<DepartmentDto[]> GetAll();
}
