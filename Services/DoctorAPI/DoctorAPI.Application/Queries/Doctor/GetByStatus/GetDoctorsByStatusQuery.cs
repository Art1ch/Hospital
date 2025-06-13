using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.Core.Enums;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetByStatus;

public record GetDoctorsByStatusQuery(DoctorStatus DoctorStatus) : IRequest<GetByStatusDoctorsResponse>;

