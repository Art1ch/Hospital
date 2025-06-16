using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Validation.BaseDoctorValidator;

namespace DoctorAPI.Application.Validation.Validators.Doctor;

public class CreateDoctorRequestValidator : BaseDoctorValidator<CreateDoctorRequest>
{
    public CreateDoctorRequestValidator()
    {
        ValidateFirstName(x => x.FirstName);
        ValidateLastName(x => x.LastName);
        ValidateMiddleName(x => x.MiddleName);
        ValidateBirthDate(x => x.BirthDate);
        ValidateCareerStartDate(x => x.CareerStartDay);
        ValidateStatus(x => x.Status);
        ValidateSpecializationName(x => x.SpecializationName);
    }
}
