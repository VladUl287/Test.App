using Test.Core.Dtos;
using FluentValidation;
using Test.Core.Contracts.Repositories;
using FluentValidation.Results;

namespace Test.Core.Validators;

public sealed class EmployeeValidator : AbstractValidator<EmployeeDto>
{
    private readonly IDepartmentPresenter departmentPresenter;

    public EmployeeValidator(IDepartmentPresenter departmentPresenter)
    {
        this.departmentPresenter = departmentPresenter;

        RuleFor(p => p.FullName)
            .NotEmpty().WithMessage("Пустое ФИО.")
            .MaximumLength(255).WithMessage("Слишком длинное ФИО.");

        RuleFor(p => p.Salary)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Некорректное значение заработной платы.");

        RuleFor(p => p.Department)
            .NotNull()
            .WithMessage("Не предоставлены данные об Отделе.");
    }

    public override async Task<ValidationResult> ValidateAsync(ValidationContext<EmployeeDto> context, CancellationToken cancellation = default)
    {
        var employee = context.InstanceToValidate;

        var result = await base.ValidateAsync(context, cancellation);
        if (!result.IsValid)
        {
            return result;
        }

        var exists = await departmentPresenter.Exists(employee.Department.Id);
        if (!exists)
        {
            result.Errors.Add(new(nameof(EmployeeDto.Department), "Отдел не найден."));
            return result;
        }

        return result;
    }
}
