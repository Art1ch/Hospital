using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Validation.BaseValidators;

namespace AuthAPI.Application.Validation.Validators.Account;

public class RegistrationValidator : AccountBaseValidator<RegistrationRequest>
{
    public RegistrationValidator()
    {
        ValidateEmail(x => x.Email);
        ValidatePassword(x => x.Password!);
        ValidatePhoneNumber(x => x.PhoneNumber!);
    }
}
