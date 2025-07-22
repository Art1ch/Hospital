using OfficesAPI.Application.Requests.Office;
using OfficesAPI.Application.Validation.BaseValidators;

namespace OfficesAPI.Application.Validation.Validators;

public class ChangeOfficeStatusRequestValidator : BaseOfficeValidator<ChangeOfficeStatusRequest>
{
    public ChangeOfficeStatusRequestValidator()
    {
        ValidateOfficeStatus(x => x.Status);
    }
}
