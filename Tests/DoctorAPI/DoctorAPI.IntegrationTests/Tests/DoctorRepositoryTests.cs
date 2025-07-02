using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;
using DoctorAPI.Infrastructure.Repositories.Implementations;
using DoctorAPI.IntegrationTests.Fixtures;

namespace DoctorAPI.IntegrationTests.Tests;

public class DoctorRepositoryTests 
{
    private readonly IDoctorRepository _doctorRepository;
    public DoctorRepositoryTests()
    {
        var fixture = new PostgreSqlTestContainerFixture();
        _doctorRepository = new DoctorRepository(fixture._dbContext);
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

        await _doctorRepository.CreateAsync(doctor);

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
        var savedDoctor = await _doctorRepository.GetAsync(doctor.Id);
        Assert.NotNull(savedDoctor);
        Assert.Equal(doctor.FirstName, savedDoctor.FirstName);
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

        await _doctorRepository.CreateAsync(doctor);

        // Act
        doctor.FirstName = "TestUpdatedFirstName";
        await _doctorRepository.UpdateAsync(doctor);

        // Assert
        var updatedDoctor = await _doctorRepository.GetAsync(doctor.Id);
        Assert.Equal(doctor.FirstName, updatedDoctor!.FirstName);
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

        await _doctorRepository.CreateAsync(doctor);

        // Act
        await _doctorRepository.DeleteAsync(doctor.Id);

        // Assert
        var deletedDoctor = await _doctorRepository.GetAsync(doctor.Id);
        Assert.Null(deletedDoctor);
    }
}
