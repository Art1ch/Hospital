using AuthAPI.Application.Validation.Constants;
using FluentValidation;
using System.Linq.Expressions;

namespace AuthAPI.Application.Validation.BaseValidators;

public class AccountBaseValidator<T> : AbstractValidator<T>
{
    protected void ValidateEmail(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithErrorCode(ErrorCodeConstants.EmailRequired)
            .EmailAddress()
            .WithErrorCode(ErrorCodeConstants.EmailInvalid);
    }

    protected IRuleBuilderOptions<T, string> ValidatePhoneNumber(Expression<Func<T, string>> expression)
    {
        return RuleFor(expression)
            .MinimumLength(AccountConstants.MinPhoneNumberLength)
            .WithErrorCode(ErrorCodeConstants.PhoneTooShort)
            .MaximumLength(AccountConstants.MaxPhoneNumberLength)
            .WithErrorCode(ErrorCodeConstants.PhoneTooLong);
    }

    protected void ValidatePassword(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithErrorCode(ErrorCodeConstants.PasswordRequired)
            .MinimumLength(AccountConstants.MinPasswordLength)
            .WithErrorCode(ErrorCodeConstants.PasswordTooShort)
            .MaximumLength(AccountConstants.MaxPasswordLength)
            .WithErrorCode(ErrorCodeConstants.PasswordTooLong)
            .Matches(@"[A-Z]")
            .WithErrorCode(ErrorCodeConstants.PasswordUppercaseRequired)
            .Matches(@"[a-z]")
            .WithErrorCode(ErrorCodeConstants.PasswordLowercaseRequired)
            .Matches(@"\d")
            .WithErrorCode(ErrorCodeConstants.PasswordNumberRequired)
            .Matches(@"^\S+$")
            .WithErrorCode(ErrorCodeConstants.PasswordNoWhitespace);
    }
}
