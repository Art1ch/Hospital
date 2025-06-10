using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public record GetByStatusDoctorsQuery(
   GetByStatusDoctorsRequestDto Dto) 
    : IRequest<GetByStatusDoctorsResponseDto>;

