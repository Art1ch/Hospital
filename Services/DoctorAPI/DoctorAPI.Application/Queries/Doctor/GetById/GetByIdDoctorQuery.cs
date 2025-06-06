using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

public record GetByIdDoctorQuery<TDoctorId,
    TSpecializationId>(
    GetByIdDoctorRequestDto<TDoctorId> Dto)
    : IRequest<GetByIdDoctorResponseDto<TDoctorId,
        TSpecializationId>>;
