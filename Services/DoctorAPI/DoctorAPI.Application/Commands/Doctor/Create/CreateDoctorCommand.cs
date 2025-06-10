using DoctorAPI.Application.WebRequests.Doctor.Create;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public record CreateDoctorCommand(
    CreateDoctorRequestDto Dto)
    : IRequest<CreateDoctorResponseDto>;

