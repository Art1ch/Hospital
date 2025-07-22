using OfficesAPI.Commands.Application.Requests.Office;
using OfficesAPI.Commands.Application.Validation.BaseValidators;

namespace OfficesAPI.Commands.Application.Validation.Validators;

public class ChangeOfficeStatusRequestValidator : BaseOfficeValidator<ChangeOfficeStatusRequest>
{
    public ChangeOfficeStatusRequestValidator()
    {
        ValidateOfficeStatus(x => x.Status);
    }
}
