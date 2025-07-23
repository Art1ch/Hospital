using OfficesAPI.Queries.Application.Requests.Office;
using OfficesAPI.Queries.Application.Validation.BaseValidators;

namespace OfficesAPI.Queries.Application.Validation.Validators;

public class GetAllOfficesRequestValidator : BasePaginationValidator<GetAllOfficesRequest>
{
    public GetAllOfficesRequestValidator()
    {
        ValidatePageNumber(x => x.Page);
        ValidatePageSize(x => x.PageSize);
    }
}
