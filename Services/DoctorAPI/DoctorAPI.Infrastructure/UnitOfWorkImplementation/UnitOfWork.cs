using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Contracts.Repository.Specialization;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Infrastructure.Context;

namespace DoctorAPI.Infrastructure.UnitOfWorkImplementation;

internal class UnitOfWork : IUnitOfWork
{
    public IDoctorRepository DoctorRepository { get; set; }
    public ISpecializationRepository SpecializationRepository { get ; set ; }
    private readonly DoctorDbContext _dbContext;

    public UnitOfWork(
        DoctorDbContext dbContext,
        IDoctorRepository doctorRepository,
        ISpecializationRepository specializationRepository)
    {

        _dbContext = dbContext;
        DoctorRepository = doctorRepository;
        SpecializationRepository = specializationRepository;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction == null)
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }
}
