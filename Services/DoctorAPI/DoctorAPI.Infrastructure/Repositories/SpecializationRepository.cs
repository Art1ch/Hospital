using DoctorAPI.Application.Contracts.Repository.Specialization;
using DoctorAPI.Core.Entities;
using DoctorAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Repositories;

internal class SpecializationRepository : ISpecializationRepository
{
    private readonly DoctorDbContext _dbContext;

    public SpecializationRepository(DoctorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(SpecializationEntity specialization, CancellationToken cancellationToken = default)
    {
        await _dbContext.Specializations.AddAsync(specialization, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbContext.Specializations
           .Where(d => d.Id!.Equals(id))
           .ExecuteDeleteAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<SpecializationEntity> GetById(int id, CancellationToken cancellationToken)
    {
        var specialization = await _dbContext.Specializations
            .AsNoTracking()
            .Include(d => d.Doctor)
            .Where(d => d.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return specialization ?? throw new KeyNotFoundException();
    }

    public async Task UpdateAsync(SpecializationEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Specializations
            .Where(s => s.Id == entity.Id)
            .ExecuteUpdateAsync(set => set
            .SetProperty(s => s.Name, entity.Name));
    }
}
