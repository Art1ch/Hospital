using DoctorAPI.Application.WebRequests.Doctor.Create;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public record CreateDoctorCommand<TDoctorId, TSpecializationId>(
    CreateDoctorRequestDto<TDoctorId,
        TSpecializationId> Dto) : IRequest<CreateDoctorResponseDto<TDoctorId>>;

