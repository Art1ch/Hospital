using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public record GetByStatusDoctorsQuery<TDoctorId,
    TSpecializationId>(
   GetByStatusDoctorsRequestDto Dto) 
    : IRequest<GetByStatusDoctorsResponseDto<TDoctorId,
        TSpecializationId>>;

