using AppointmentAPI.Application.Abstractions;
using AppointmentAPI.Application.Responses.Appointment;

namespace AppointmentAPI.Application.Queries.Appointment.GetDoctorsSchedule;

public sealed record GetDoctorsAppointmentScheduleQuery(
    Guid DoctorId
) : BaseRequest<Guid, GetDoctorsAppointmentScheduleResponse>(DoctorId);