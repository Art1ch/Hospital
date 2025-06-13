using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Validation.BaseDoctorValidator;

namespace DoctorAPI.Application.Validation.Validators.Doctor;

public class GetAllDoctorsRequestValidator : BaseDoctorValidator<GetAllDoctorsRequest>
{
    public GetAllDoctorsRequestValidator()
    {
        ValidatePageNumber(x => x.Page);
        ValidatePageSize(x => x.PageSize);
    }
}
