using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using Test.Core.Dtos;

namespace Test.Core.Contracts.Services;

public interface IEmployeeService
{
    Task<OneOf<Success, ValidationResult, Error<string>>> Create(EmployeeDto employeeDto);

    Task<OneOf<Success, ValidationResult, NotFound, Error<string>>> Update(EmployeeDto employeeDto);

    Task<OneOf<Success, NotFound>> Delete(Guid id);
}
