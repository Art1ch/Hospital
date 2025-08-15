using AppointmentAPI.Application.Requests.Appointment;
using AppointmentAPI.Application.Validation.BaseValidators;

namespace AppointmentAPI.Application.Validation.Validators;

public sealed class ChangeAppointmentStatusValidator : BaseAppointmentValidator<ChangeAppointmentsStatusRequest>
{
    public ChangeAppointmentStatusValidator()
    {
        ValidateAppointmentStatus(x => x.Status);
    }
}
