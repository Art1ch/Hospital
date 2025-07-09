using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.IntegrationTests.Fixtures;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DoctorAPI.IntegrationTests.Tests;

public class CacheServiceTests : IClassFixture<RedisContainerFixture>
{
    private readonly ICacheService _cacheService;
    private readonly IDistributedCache _cacheMemory;

    public CacheServiceTests(RedisContainerFixture fixture)
    {
        _cacheService = fixture.CacheService;
        _cacheMemory = fixture.CacheMemory;
    }

    [Fact]
    public async Task SetAsync_WhenValidDataProvided_ShouldSetResponse()
    {
        //Arrange
        var doctorInfo = new GetDoctorInfoByIdResult
        (
            Guid.NewGuid(),
            "TestFirstName",
            "TestLastName",
            "TestMiddleName",
            DoctorStatus.Available,
            new DateOnly(1990, 1, 1),
            new DateOnly(2015, 1, 1),
            new SpecializationEntity
            {
                Name = "TestSpecializationName"
            }
        );
        var doctorResponse = new GetByIdDoctorResponse(doctorInfo);
        var id = doctorResponse.Doctor.Id;

        //Act
        await _cacheService.SetAsync(id.ToString(), doctorResponse);

        //Assert
        var cachedString = await _cacheMemory.GetStringAsync(id.ToString());
        var cachedDoctor = JsonSerializer.Deserialize<GetByIdDoctorResponse>(cachedString!);

        Assert.Equal(doctorResponse.Doctor.Id, cachedDoctor.Doctor.Id);
        Assert.Equal(doctorResponse.Doctor.FirstName, cachedDoctor.Doctor.FirstName);
        Assert.Equal(doctorResponse.Doctor.MiddleName, cachedDoctor.Doctor.MiddleName);
        Assert.Equal(doctorResponse.Doctor.LastName, cachedDoctor.Doctor.LastName);
        Assert.Equal(doctorResponse.Doctor.BirthDate, cachedDoctor.Doctor.BirthDate);
        Assert.Equal(doctorResponse.Doctor.Status, cachedDoctor.Doctor.Status);
        Assert.Equal(doctorResponse.Doctor.CareerStartDay, cachedDoctor.Doctor.CareerStartDay);
    }

    [Fact]
    public async Task GetAsync_WhenValidDataProvided_ShouldReturnResponse()
    {
        //Arrange
        var doctorInfo = new GetDoctorInfoByIdResult
        (
            Guid.NewGuid(),
            "TestFirstName",
            "TestLastName",
            "TestMiddleName",
            DoctorStatus.Available,
            new DateOnly(1990, 1, 1),
            new DateOnly(2015, 1, 1),
            new SpecializationEntity
            {
                Name = "TestSpecializationName"
            }
        );
        var doctorResponse = new GetByIdDoctorResponse(doctorInfo);
        var id = doctorResponse.Doctor.Id;

        var doctorString = JsonSerializer.Serialize(doctorResponse);
        await _cacheMemory.SetStringAsync(id.ToString(), doctorString);

        //Act
        var cachedDoctor = await _cacheService.GetAsync<GetByIdDoctorResponse>(id.ToString());

        //Assert
        Assert.Equal(doctorResponse.Doctor.Id, cachedDoctor.Doctor.Id);
        Assert.Equal(doctorResponse.Doctor.FirstName, cachedDoctor.Doctor.FirstName);
        Assert.Equal(doctorResponse.Doctor.MiddleName, cachedDoctor.Doctor.MiddleName);
        Assert.Equal(doctorResponse.Doctor.LastName, cachedDoctor.Doctor.LastName);
        Assert.Equal(doctorResponse.Doctor.BirthDate, cachedDoctor.Doctor.BirthDate);
        Assert.Equal(doctorResponse.Doctor.Status, cachedDoctor.Doctor.Status);
        Assert.Equal(doctorResponse.Doctor.CareerStartDay, cachedDoctor.Doctor.CareerStartDay);
    }

    [Fact]
    public async Task RemoveAsync_WhenValidKeyProvided_ShouldRemoveResponse()
    {
        //Arrange
        var doctorInfo = new GetDoctorInfoByIdResult
        (
            Guid.NewGuid(),
            "TestFirstName",
            "TestLastName",
            "TestMiddleName",
            DoctorStatus.Available,
            new DateOnly(1990, 1, 1),
            new DateOnly(2015, 1, 1),
            new SpecializationEntity
            {
                Name = "TestSpecializationName"
            }
        );
        var doctorResponse = new GetByIdDoctorResponse(doctorInfo);
        var id = doctorResponse.Doctor.Id;

        var doctorString = JsonSerializer.Serialize(doctorResponse);
        await _cacheMemory.SetStringAsync(id.ToString(), doctorString);

        //Act
        await _cacheService.RemoveAsync(id.ToString());

        //Assert
        var cachedDoctor = _cacheMemory.GetString(id.ToString());
        Assert.Null(cachedDoctor);
    }
}
