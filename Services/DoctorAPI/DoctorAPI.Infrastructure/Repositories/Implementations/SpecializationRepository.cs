using DoctorAPI.Application.Contracts.Repository.Specialization;
using DoctorAPI.Core.Entities;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Repositories.Implementations;

internal class SpecializationRepository : Repository<SpecializationEntity, int>, ISpecializationRepository
{
    private readonly DoctorDbContext _dbContext;

    public SpecializationRepository(DoctorDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
