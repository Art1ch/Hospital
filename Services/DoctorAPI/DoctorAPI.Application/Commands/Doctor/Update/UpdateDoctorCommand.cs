using DoctorAPI.Application.Requests.Doctor;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public record UpdateDoctorCommand(UpdateDoctorRequest Request) : IRequest;
