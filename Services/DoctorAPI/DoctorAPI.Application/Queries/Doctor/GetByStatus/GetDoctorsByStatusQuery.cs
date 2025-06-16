using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.Application.Enums;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public record GetDoctorsByStatusQuery(DoctorStatus DoctorStatus) : IRequest<GetByStatusDoctorsResponse>;

