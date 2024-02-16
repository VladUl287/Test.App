using Microsoft.AspNetCore.Mvc;
using Test.Core.Contracts.Repositories;
using Test.Core.Contracts.Services;
using Test.Core.Dtos;

namespace Test.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class DepartamentController : ControllerBase
{
    private readonly IDepartmentPresenter departmentPresenter;

    public DepartamentController(IDepartmentPresenter departmentPresenter)
    {
        this.departmentPresenter = departmentPresenter;
    }

    [HttpGet]
    public async Task<IEnumerable<DepartmentDto>> GetAll()
    {
        return await departmentPresenter.GetAll();
    }
}
