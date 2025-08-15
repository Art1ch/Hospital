using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetEntitiesQuery;

internal class GetDoctorEntitiesQueryHandler(
    IDoctorRepository doctorRepository
) : IRequestHandler<GetDoctorEntitiesQuery, IQueryable<DoctorEntity>>
{
    public Task<IQueryable<DoctorEntity>> Handle(GetDoctorEntitiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            doctorRepository.GetEntitiesQuery());
    }
}
