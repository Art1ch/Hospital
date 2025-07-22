using OfficesAPI.Application.Requests.Office;
using OfficesAPI.Application.Validation.BaseValidators;

namespace OfficesAPI.Application.Validation.Validators;

public class GetAllOfficesRequestValidator : BaseOfficeValidator<GetAllOfficesRequest>
{
    public GetAllOfficesRequestValidator()
    {
        ValidatePageNumber(x => x.Page);
        ValidatePageSize(x => x.PageSize);
    }
}
