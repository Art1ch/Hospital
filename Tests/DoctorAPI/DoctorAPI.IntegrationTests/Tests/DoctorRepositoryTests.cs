using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.Infrastructure.Repositories.Implementations;
using DoctorAPI.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.IntegrationTests.Tests;

public class DoctorRepositoryTests : IClassFixture<PostgresContainerFixture>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly DbSet<DoctorEntity> _doctors;
    private readonly DoctorDbContext _context;

    public DoctorRepositoryTests(PostgresContainerFixture container)
    {
        _doctorRepository = new DoctorRepository(container.DbContext);
        _doctors = container.DbContext.Doctors;
        _context = container.DbContext;
    }

    [Fact]
    public async Task GetAsync_WhenValidIdProvided_ShouldReturnDoctor()
    {
        // Arrange
        var doctor = new DoctorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            MiddleName = "TestMiddleName",
            BirthDate = new DateOnly(1990, 1, 1),
            CareerStartDay = new DateOnly(2015, 1, 1),
            Status = DoctorStatus.Available,
            Specialization = new SpecializationEntity
            {
                Name = "TestSpecializationName"
            }
        };

        await _doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();

        // Act
        var result = await _doctorRepository.GetAsync(doctor.Id);

        // Assert
        Assert.Equal(doctor.Id, result.Id);
        Assert.Equal(doctor.FirstName, result.FirstName);
        Assert.Equal(doctor.LastName, result.LastName);
        Assert.Equal(doctor.MiddleName, result.MiddleName);
        Assert.Equal(doctor.Status, result.Status);
    }

    [Fact]
    public async Task CreateAsync_WhenValidDataProvided_ShouldCreateDoctor()
    {
        // Arrange
        var doctor = new DoctorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            MiddleName = "TestMiddleName",
            BirthDate = new DateOnly(1990, 1, 1),
            CareerStartDay = new DateOnly(2015, 1, 1),
            Status = DoctorStatus.Available,
            Specialization = new SpecializationEntity
            {
                Name = "TestSpecializationName"
            }
        };

        // Act
        await _doctorRepository.CreateAsync(doctor);

        // Assert
        var savedDoctor = await _doctors.Where(d => d.Id == doctor.Id).FirstAsync();

        Assert.NotNull(savedDoctor);
        Assert.Equal(doctor.Id, savedDoctor.Id);
        Assert.Equal(doctor.Status, savedDoctor.Status);
        Assert.Equal(doctor.FirstName, savedDoctor.FirstName);
        Assert.Equal(doctor.MiddleName, savedDoctor.MiddleName);
        Assert.Equal(doctor.LastName, savedDoctor.LastName);
    }

    [Fact]
    public async Task UpdateAsync_WhenValidDataProvided_ShouldUpdateDoctor()
    {
        // Arrange
        var doctor = new DoctorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "TestNotUpdatedFirstName",
            LastName = "TestNotUpdatedLastName",
            MiddleName = "TestNotUpdatedMiddleName",
            BirthDate = new DateOnly(1990, 1, 1),
            CareerStartDay = new DateOnly(2015, 1, 1),
            Status = DoctorStatus.Available,
            Specialization = new SpecializationEntity
            {
                Name = "TestSpecializationName"
            }
        };

        await _doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        doctor.FirstName = "TestUpdatedFirstName";

        // Act
        await _doctorRepository.UpdateAsync(doctor);

        // Assert
        var updatedDoctor = await _doctors.Where(d => d.Id == doctor.Id).FirstAsync();

        Assert.NotNull(updatedDoctor);
        Assert.Equal(doctor.Id, updatedDoctor.Id);
        Assert.Equal(doctor.Status, updatedDoctor.Status);
        Assert.Equal(doctor.FirstName, updatedDoctor.FirstName);
        Assert.Equal(doctor.MiddleName, updatedDoctor.MiddleName);
        Assert.Equal(doctor.LastName, updatedDoctor.LastName);
    }

    [Fact]
    public async Task DeleteAsync_WhenValidIdProvided_ShouldDeleteDoctor()
    {
        // Arrange
        var doctor = new DoctorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "TestNotUpdatedFirstName",
            LastName = "TestNotUpdatedLastName",
            MiddleName = "TestNotUpdatedMiddleName",
            BirthDate = new DateOnly(1990, 1, 1),
            CareerStartDay = new DateOnly(2015, 1, 1),
            Status = DoctorStatus.Available,
            Specialization = new SpecializationEntity
            {
                Name = "TestSpecializationName"
            }
        };

        await _doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();

        // Act
        await _doctorRepository.DeleteAsync(doctor.Id);

        // Assert
        var deletedDoctor = await _doctors.Where(d => d.Id == doctor.Id).FirstOrDefaultAsync();
        Assert.Null(deletedDoctor);
    }
}
