using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;
using DoctorAPI.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DoctorAPI.IntegrationTests.Tests;

public class DoctorRepositoryTests : IClassFixture<PostgreSqlTestContainerFixture>, IAsyncLifetime
{
    private readonly PostgreSqlTestContainerFixture _fixture;
    private IDbContextTransaction _transaction;

    public DoctorRepositoryTests(PostgreSqlTestContainerFixture fixture)
    {
        _fixture = fixture;
    }

    public async Task InitializeAsync()
    {
        _transaction = await _fixture.DbContext.Database.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
    }

    [Fact]
    public async Task GetById_Should_Return_Doctor()
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

        _fixture.DbContext.Doctors.Add(doctor);
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        var result = await _fixture.DbContext.Doctors
            .Where(d => d.Id == doctor.Id)
            .Include(d => d.Specialization)
            .FirstAsync();

        // Assert
        Assert.Equal(doctor.Id, result.Id);
        Assert.Equal(doctor.FirstName, result.FirstName);
        Assert.Equal(doctor.LastName, result.LastName);
        Assert.Equal(doctor.MiddleName, result.MiddleName);
        Assert.Equal(doctor.Status, result.Status);
        Assert.Equal(doctor.Specialization.Name, result.Specialization.Name);
    }

    [Fact]
    public async Task Create_Should_Add_Doctor_To_Database()
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
        _fixture.DbContext.Doctors.Add(doctor);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var savedDoctor = await _fixture.DbContext.Doctors.FindAsync(doctor.Id);
        Assert.NotNull(savedDoctor);
        Assert.Equal(doctor.FirstName, savedDoctor.FirstName);
    }

    [Fact]
    public async Task Update_Should_Modify_Existing_Doctor()
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

        _fixture.DbContext.Doctors.Add(doctor);
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        doctor.FirstName = "TestUpdatedFirstName";
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var updatedDoctor = await _fixture.DbContext.Doctors.FindAsync(doctor.Id);
        Assert.Equal(doctor.FirstName, updatedDoctor!.FirstName);
    }

    [Fact]
    public async Task Delete_Should_Remove_Doctor_From_Database()
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

        _fixture.DbContext.Doctors.Add(doctor);
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        _fixture.DbContext.Doctors.Remove(doctor);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var deletedDoctor = await _fixture.DbContext.Doctors.FindAsync(doctor.Id);
        Assert.Null(deletedDoctor);
    }
}
