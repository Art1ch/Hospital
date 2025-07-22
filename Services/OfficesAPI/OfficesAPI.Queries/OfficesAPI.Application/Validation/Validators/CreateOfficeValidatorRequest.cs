using OfficesAPI.Application.Requests.Office;
using OfficesAPI.Application.Validation.BaseValidators;

namespace OfficesAPI.Application.Validation.Validators;

public class CreateOfficeValidatorRequest : BaseOfficeValidator<CreateOfficeRequest>
{
    public CreateOfficeValidatorRequest()
    {
        ValidateAddressName(x => x.Address);
        ValidatePhoneNumber(x => x.RegistryPhoneNumber);
        ValidateOfficeStatus(x => x.Status);
    }
}
