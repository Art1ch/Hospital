using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Application.WebRequests.Doctor.Update;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public record UpdateDoctorCommand<TDoctorId,
    TSpecializationId>(
    UpdateDoctorRequestDto<TDoctorId,
        TSpecializationId> Dto) : IRequest<UpdateDoctorResponseDto<TDoctorId>>;
