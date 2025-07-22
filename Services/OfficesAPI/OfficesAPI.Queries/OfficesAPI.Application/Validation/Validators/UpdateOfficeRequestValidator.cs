using OfficesAPI.Application.Requests.Office;
using OfficesAPI.Application.Validation.BaseValidators;

namespace OfficesAPI.Application.Validation.Validators;

public class UpdateOfficeRequestValidator : BaseOfficeValidator<UpdateOfficeRequest>
{
    public UpdateOfficeRequestValidator()
    {
        ValidateAddressName(x => x.Address);
        ValidatePhoneNumber(x => x.RegistryPhoneNumber);
        ValidateOfficeStatus(x => x.Status);
    }
}
