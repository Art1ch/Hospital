using AppointmentAPI.Application.RepositoryResults.Appointment;

namespace AppointmentAPI.Application.Responses.Appointment;

public record GetDoctorsAppointmentScheduleResponse(
    GetDoctorsAppointmentScheduleResult Result
);
