using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Notifications.Interfaces;
using AppointmentAPI.Email.Interfaces;

namespace AppointmentAPI.Notifications.Implementations;

internal class AppointmentEmailNotificationService : IAppointmentNotificationService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IEmailSender _emailSender;
    //injection of EventBus?

    public AppointmentEmailNotificationService(IAppointmentRepository appointmentRepository, IEmailSender emailSender)
    {
        _appointmentRepository = appointmentRepository;
        _emailSender = emailSender;
    }

    public Task NotifyAboutAppointment()
    {
        throw new NotImplementedException();
    }
}
