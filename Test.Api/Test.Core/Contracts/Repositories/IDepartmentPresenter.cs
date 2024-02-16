namespace Test.Core.Contracts.Repositories;

public interface IDepartmentPresenter
{
    Task<bool> Exists(long id);
}
