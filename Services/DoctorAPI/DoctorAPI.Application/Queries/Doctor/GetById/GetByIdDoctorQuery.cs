using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetById;

public record GetByIdDoctorQuery(Guid Id): IRequest<GetByIdDoctorResponse>;
