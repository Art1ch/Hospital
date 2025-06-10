using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

public record GetBySpecializationDoctorQuery(
    GetBySpecializationDoctorRequestDto Dto)
    : IRequest<GetBySpecializationDoctorResponseDto>;
