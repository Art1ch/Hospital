using DoctorAPI.Application.Entities;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetEntitiesQuery;

public record GetDoctorEntitiesQuery : IRequest<IQueryable<DoctorEntity>>;
