using DoctorAPI.Application.Requests.Doctor;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

public record DeleteDoctorCommand(Guid Id) : IRequest;

