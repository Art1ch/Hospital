using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Validation.BaseValidators;

namespace DoctorAPI.Application.Validation.Validators.Doctor;

public class GetAllDoctorsRequestValidator : BasePaginationValidator<GetAllDoctorsRequest>
{
    public GetAllDoctorsRequestValidator()
    {
        ValidatePageNumber(x => x.Page);
        ValidatePageSize(x => x.PageSize);
    }
}
