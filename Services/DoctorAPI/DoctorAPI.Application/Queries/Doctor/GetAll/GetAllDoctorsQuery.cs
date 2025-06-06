using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

public record GetAllDoctorsQuery<TDoctorId,
    TSpecializationId>(
    int Page,
    int PageSize) 
    : IRequest<GetAllDoctorsResponseDto<TDoctorId,
        TSpecializationId>>;
