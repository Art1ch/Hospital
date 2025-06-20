using AuthAPI.Application.Requests.Token;
using AuthAPI.Application.Validation.BaseValidators;

namespace AuthAPI.Application.Validation.Validators.Token;

public class ReferenceTokenValidator : TokenBaseValidator<ExchangeTokenRequest>
{
    public ReferenceTokenValidator()
    {
        ValidateTokenValue(x => x.ReferenceToken);
    }
}
