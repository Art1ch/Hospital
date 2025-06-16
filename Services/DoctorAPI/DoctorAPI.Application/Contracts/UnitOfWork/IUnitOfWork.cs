using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Contracts.Repository.Specialization;

namespace DoctorAPI.Application.Contracts.UnitOfWork;

public interface IUnitOfWork
{
    IDoctorRepository DoctorRepository { get; }
    ISpecializationRepository SpecializationRepository { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
