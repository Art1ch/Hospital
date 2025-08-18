using AuthAPI.Application.Validation.Constants;
using FluentValidation;
using System.Linq.Expressions;

namespace AuthAPI.Application.Validation.BaseValidators;

public class TokenBaseValidator<T> : AbstractValidator<T>
{
    protected void ValidateTokenValue(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithErrorCode(ErrorCodeConstants.TokenRequired);
    }
}
