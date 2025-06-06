using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

public record GetByIdDoctorQuery<TId1, TId2>(
    GetByIdDoctorRequestDto<TId1> Dto) : IRequest<GetByIdDoctorResponseDto<TId1, TId2>>;
