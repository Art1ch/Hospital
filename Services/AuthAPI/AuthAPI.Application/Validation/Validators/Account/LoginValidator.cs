using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Validation.BaseValidators;

namespace AuthAPI.Application.Validation.Validators.Account;

public class LoginValidator : AccountBaseValidator<LoginRequest>
{
    public LoginValidator()
    {
        ValidateEmail(x => x.Email);
        ValidatePassword(x => x.Password);
    }
}
