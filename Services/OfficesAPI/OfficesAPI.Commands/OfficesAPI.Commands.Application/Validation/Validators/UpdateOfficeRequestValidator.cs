using OfficesAPI.Commands.Application.Requests.Office;
using OfficesAPI.Commands.Application.Validation.BaseValidators;

namespace OfficesAPI.Commands.Application.Validation.Validators;

public class UpdateOfficeRequestValidator : BaseOfficeValidator<UpdateOfficeRequest>
{
    public UpdateOfficeRequestValidator()
    {
        ValidateAddressName(x => x.Address);
        ValidatePhoneNumber(x => x.RegistryPhoneNumber);
        ValidateOfficeStatus(x => x.Status);
    }
}
