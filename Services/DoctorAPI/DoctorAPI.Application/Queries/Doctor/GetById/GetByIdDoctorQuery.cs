using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

public record GetByIdDoctorQuery(
    GetByIdDoctorRequestDto Dto)
    : IRequest<GetByIdDoctorResponseDto>;
