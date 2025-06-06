using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

public record GetBySpecializationDoctorQuery<TId1, TId2>(
    GetBySpecializationDoctorRequestDto<TId2> Dto)
    : IRequest<GetBySpecializationDoctorResponseDto<TId1, TId2>>;
