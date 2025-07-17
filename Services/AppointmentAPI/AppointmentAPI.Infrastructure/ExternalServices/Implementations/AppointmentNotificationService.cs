using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Infrastructure.Interfaces;

namespace AppointmentAPI.Infrastructure.Implementations;

internal class AppointmentNotificationService : IAppointmentNotificationService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IEmailSender _emailSender;

    public AppointmentNotificationService(IAppointmentRepository appointmentRepository, IEmailSender emailSender)
    {
        _appointmentRepository = appointmentRepository;
        _emailSender = emailSender;
    }

    public Task NotifyAboutAppointment()
    {
        throw new NotImplementedException();
    }
}
