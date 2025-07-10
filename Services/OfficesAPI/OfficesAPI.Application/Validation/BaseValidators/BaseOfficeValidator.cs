using FluentValidation;
using OfficesAPI.Application.Validation.Constants;
using OfficesAPI.Core.Enums;
using System.Linq.Expressions;

namespace OfficesAPI.Application.Validation.BaseValidators;

public class BaseOfficeValidator<T> : BasePaginationValidator<T>
{
    protected void ValidateAddressName(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .Length(OfficeConstants.MinAddressNameLength, OfficeConstants.MaxAddressNameLength)
            .WithMessage(ValidationConstants.OnFailedAddressNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$")
            .WithMessage(ValidationConstants.OnFailedRegexValidation);
    }

    protected void ValidatePhoneNumber(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage(ValidationConstants.OnFailedRequiredValidation)
            .NotNull()
            .WithMessage(ValidationConstants.OnFailedNullValidation)
            .MinimumLength(OfficeConstants.MinPhoneNumberLength)
            .WithMessage(ValidationConstants.OnFailedPhoneNumberValidation)
            .MaximumLength(OfficeConstants.MaxPhoneNumberLength)
            .WithMessage(ValidationConstants.OnFailedPhoneNumberValidation);
    }

    protected void ValidateOfficeStatus(Expression<Func<T, OfficeStatus>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage(ValidationConstants.OnFailedRequiredValidation)
            .NotNull()
            .WithMessage(ValidationConstants.OnFailedNullValidation);
    }
}
