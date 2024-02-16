using Test.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Test.Core.Contracts.Repositories;
using Test.Core.Contracts.Services;

namespace Test.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class EmployeeController : ControllerBase
{
    private readonly IEmployeePresenter employeePresenter;
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeePresenter employeePresenter, IEmployeeService employeeService)
    {
        this.employeePresenter = employeePresenter;
        this.employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IEnumerable<EmployeeDto>> GetAll([FromQuery] EmployeeFilters? filters)
    {
        return await employeePresenter.GetAll(filters);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDto employee)
    {
        var result = await employeeService.Create(employee);

        return result.Match<IActionResult>(
            success => Ok(),
            validation => BadRequest(validation.Errors),
            error => BadRequest(error.Value));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EmployeeDto employee)
    {
        var result = await employeeService.Update(employee);

        return result.Match<IActionResult>(
            success => NoContent(),
            validation => BadRequest(validation.Errors),
            notFound => NotFound(employee),
            error => BadRequest(error.Value));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await employeeService.Delete(id);
        return result.Match<IActionResult>(
            success => NoContent(),
            notFound => NotFound(id));
    }
}
