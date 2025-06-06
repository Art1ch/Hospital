using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

public record GetBySpecializationDoctorQuery<TDoctorId,
    TSpecializationId>(
    GetBySpecializationDoctorRequestDto<TSpecializationId> Dto)
    : IRequest<GetBySpecializationDoctorResponseDto<TDoctorId,
        TSpecializationId>>;
