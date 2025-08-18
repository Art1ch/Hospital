using FluentValidation;
using OfficesAPI.Commands.Application.Validation.Constants;
using OfficesAPI.Shared.Enum;
using System.Linq.Expressions;

namespace OfficesAPI.Commands.Application.Validation.BaseValidators;

public class BaseOfficeValidator<T> : AbstractValidator<T>
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
