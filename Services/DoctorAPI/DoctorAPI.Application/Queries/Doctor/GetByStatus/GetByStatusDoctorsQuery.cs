using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public record GetByStatusDoctorsQuery<TId1, TId2>(
   GetByStatusDoctorsRequestDto Dto) 
    : IRequest<GetByStatusDoctorsResponseDto<TId1, TId2>>;

