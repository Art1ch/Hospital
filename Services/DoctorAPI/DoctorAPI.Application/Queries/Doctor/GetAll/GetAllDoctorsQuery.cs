using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

public record GetAllDoctorsQuery<TId1, TId2>(
    int Page,
    int PageSize) : IRequest<GetAllDoctorsResponseDto<TId1, TId2>>;
