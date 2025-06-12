using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetBySpecialization;

public record GetBySpecializationDoctorQuery(int SpecializationId) : IRequest<GetBySpecializationDoctorResponse>;
