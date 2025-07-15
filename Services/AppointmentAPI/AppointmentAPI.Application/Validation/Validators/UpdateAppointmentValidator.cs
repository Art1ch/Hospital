using AppointmentAPI.Application.Requests.Appointment;
using AppointmentAPI.Application.Validation.BaseValidators;

namespace AppointmentAPI.Application.Validation.Validators;

public sealed class UpdateAppointmentValidator : BaseAppointmentValidator<UpdateAppointmentRequest>
{
    public UpdateAppointmentValidator()
    {
        ValidateAppointmentDate(x => x.Date);
        ValidateAppointmentTime(x => x.StartAppointmentTime);
        ValidateAppointmentTime(x => x.EndAppointmentTime);
    }
}
