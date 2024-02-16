using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Test.Core.Contracts.Repositories;
using Test.Core.Contracts.Services;
using Test.Core.Dtos;

namespace Test.Core.Services;

public sealed class EmployeeService : IEmployeeService
{
    private readonly ILogger<EmployeeService> logger;
    private readonly IValidator<EmployeeDto> validator;
    private readonly IEmployeeManager employeeManager;
    private readonly IEmployeePresenter employeePresenter;
    private readonly IDepartmentPresenter departmentPresenter;

    public EmployeeService(
        ILogger<EmployeeService> logger, 
        IValidator<EmployeeDto> validator,
        IEmployeeManager employeeManager,
        IEmployeePresenter employeePresenter,
        IDepartmentPresenter departmentPresenter)
    {
        this.logger = logger;
        this.validator = validator;
        this.employeeManager = employeeManager;
        this.employeePresenter = employeePresenter;
        this.departmentPresenter = departmentPresenter;
    }

    public async Task<OneOf<Success, ValidationResult, Error<string>>> Create(EmployeeDto employeeDto)
    {
        try
        {
            var validation = await validator.ValidateAsync(employeeDto);
            if (!validation.IsValid)
            {
                return validation;
            }

            await employeeManager.Create(new()
            {
                Salary = employeeDto.Salary,
                FullName = employeeDto.FullName,
                DateBirth = employeeDto.DateBirth,
                DateEmployment = employeeDto.DateEmployment,
                DepartmentId = employeeDto.Department!.Id
            });

            return new Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            return new Error<string>("Ошибка создания работника. Попробуйте позже.");
        }
    }

    public async Task<OneOf<Success, ValidationResult, NotFound, Error<string>>> Update(EmployeeDto employeeDto)
    {
        try
        {
            var validation = await validator.ValidateAsync(employeeDto);
            if (!validation.IsValid)
            {
                return validation;
            }

            var employee = await employeePresenter.Get(employeeDto.Id);
            if (employee is null)
            {
                return new NotFound();
            }

            employee.Salary = employeeDto.Salary;
            employee.FullName = employeeDto.FullName;
            employee.DateBirth = employeeDto.DateBirth;
            employee.DateEmployment = employeeDto.DateEmployment;
            employee.DepartmentId = employeeDto.Department.Id;

            await employeeManager.Update(employee);

            return new Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            return new Error<string>("Ошибка обновления данных работника. Попробуйте позже.");
        }
    }

    public async Task<OneOf<Success, NotFound>> Delete(Guid id)
    {
        var exists = await employeePresenter.Exists(id);
        if (!exists)
        {
            return new NotFound();
        }

        await employeeManager.Delete(id);
        return new Success();
    }
}
