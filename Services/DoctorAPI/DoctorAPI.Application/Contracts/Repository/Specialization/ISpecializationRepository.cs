using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.Contracts.Repository.Specialization;

public interface ISpecializationRepository : IRepository<SpecializationEntity, int>
{
    Task<SpecializationEntity> GetById(int id, CancellationToken cancellationToken = default);
}
