using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

public record GetAllDoctorsQuery(
    GetAllDoctorsRequestDto Dto) 
    : IRequest<GetAllDoctorsResponseDto>;
