using MediatR;
using Microsoft.AspNetCore.Mvc;
using AppointmentAPI.Application.Queries.Appointment.GetDoctorsSchedule;
using AppointmentAPI.Application.Requests.Appointment;
using AppointmentAPI.Application.Commands.Appointment.Create;
using AppointmentAPI.Application.Commands.Appointment.Update;
using AppointmentAPI.Application.Commands.Appointment.Delete;
using AppointmentAPI.Application.Commands.Appointment.ChangeStatus;

namespace AppointmentAPI.Endpoints;

public static class AppointmentEndpoints
{
    public static IEndpointRouteBuilder MapAppointmentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/appointment");

        group.MapGet("/doctors-schedule", async ([FromServices] ISender sender, [FromQuery] Guid id) =>
        {
            var query = new GetDoctorsAppointmentScheduleQuery(id);
            var response = await sender.Send(query);
            return response;
        });

        group.MapPost("/", async ([FromServices] ISender sender, [FromBody] CreateAppointmentRequest request) =>
        {
            var command = new CreateAppointmentCommand(request);
            await sender.Send(command);
        });

        group.MapPatch("/", async ([FromServices] ISender sender, [FromBody] UpdateAppointmentRequest request) =>
        {
            var command = new UpdateAppointmentCommand(request);
            await sender.Send(command);
        });

        group.MapPatch("/change-appointments-status",
            async (
            [FromServices] ISender sender,
            [FromBody] ChangeAppointmentsStatusRequest request
            ) =>
        {
            var command = new ChangeAppointmentsStatusCommand(request);
            await sender.Send(command);
        });

        group.MapDelete("/", async ([FromServices] ISender sender, [FromQuery] Guid id) =>
        {
            var command = new DeleteAppointmentCommand(id);
            await sender.Send(command);
        });

        return routes;
    }
}
