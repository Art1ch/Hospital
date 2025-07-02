using FluentValidation;
using OfficesAPI.Application.Validation.Constants;
using System.Linq.Expressions;

namespace OfficesAPI.Application.Validation.BaseValidators;

public class BasePaginationValidator<T> : AbstractValidator<T>
{
    protected void ValidatePageNumber(Expression<Func<T, int>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .GreaterThanOrEqualTo(PaginationConstants.MinPageNumber).WithMessage(ValidationConstants.OnFailedPageNumberValidation);
    }

    protected void ValidatePageSize(Expression<Func<T, int>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .InclusiveBetween(PaginationConstants.MinPageSize, PaginationConstants.MaxPageSize)
            .WithMessage(ValidationConstants.OnFailedPageSizeValidation);
    }
}
