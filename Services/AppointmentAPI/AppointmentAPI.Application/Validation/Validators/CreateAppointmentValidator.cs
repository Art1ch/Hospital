using AppointmentAPI.Application.Requests.Appointment;
using AppointmentAPI.Application.Validation.BaseValidators;

namespace AppointmentAPI.Application.Validation.Validators;

public sealed class CreateAppointmentValidator : BaseAppointmentValidator<CreateAppointmentRequest>
{
    public CreateAppointmentValidator()
    {
        ValidateAppointmentDate(x => x.Date);
        ValidateAppointmentTime(x => x.StartAppointmentTime);
        ValidateAppointmentTime(x => x.EndAppointmentTime);
    }
}
