using AuthAPI.Application.Validation.Constants;
using FluentValidation;
using System.Linq.Expressions;

namespace AuthAPI.Application.Validation.BaseValidators;

public class AccountBaseValidator<T> : AbstractValidator<T>
{
    protected void ValidateEmail(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .EmailAddress().WithMessage(ValidationConstants.OnFailedEmailValidation);
    }

    protected void ValidatePhoneNumber(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .MinimumLength(AccountConstants.MinPhoneNumberLength).WithMessage(ValidationConstants.OnFailedPhoneNumberValidation)
            .MaximumLength(AccountConstants.MaxPhoneNumberLength).WithMessage(ValidationConstants.OnFailedPhoneNumberValidation);
    }

    protected void ValidatePassword(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .MinimumLength(AccountConstants.MinPasswordLength).WithMessage(ValidationConstants.OnFailedPasswordValidation)
            .MaximumLength(AccountConstants.MaxPasswordLength).WithMessage(ValidationConstants.OnFailedPasswordValidation)
            .Matches(@"[A-Z]").WithMessage(ValidationConstants.OnFailedPasswordValidation)
            .Matches(@"[a-z]").WithMessage(ValidationConstants.OnFailedPasswordValidation)
            .Matches(@"\d").WithMessage(ValidationConstants.OnFailedPasswordValidation)
            .Matches(@"^\S+$").WithMessage(ValidationConstants.OnFailedPasswordValidation);
    }

}
