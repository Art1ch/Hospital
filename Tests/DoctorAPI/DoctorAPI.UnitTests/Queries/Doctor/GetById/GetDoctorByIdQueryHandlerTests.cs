using AutoMapper;
using DoctorAPI.Application.Queries.Doctor.GetById;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using Moq;
using DoctorAPI.Application.Enums;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.Responses.Doctor;

namespace DoctorAPI.UnitTests.Queries.Doctor.GetById;

public class GetDoctorByIdQueryHandlerTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock = new();
    private readonly GetDoctorByIdQueryHandler _handler;

    public GetDoctorByIdQueryHandlerTests()
    {
        _handler = new GetDoctorByIdQueryHandler(_mapperMock.Object, _doctorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Doctor_With_Correct_Data()
    {
        // Arrange
        var id = Guid.NewGuid();
        var query = new GetDoctorByIdQuery(id);

        var testBirthDate = new DateOnly(1990, 1, 1);
        var testCareerStartDate = new DateOnly(2015, 1, 1);

        var repositoryResult = new GetDoctorInfoByIdResult(
            Id: id,
            FirstName: "TestFirstName",
            LastName: "TestLastName",
            MiddleName: "TestMiddleName",
            Status: DoctorStatus.Available,
            BirthDate: testBirthDate,
            CareerStartDay: testCareerStartDate,
            Specialization: new SpecializationEntity { Name = "TestSpecializationName" }
        );

        var expectedResponse = new GetByIdDoctorResponse(repositoryResult);

        _doctorRepositoryMock.Setup(r =>
            r.GetDoctorInfoById(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryResult);

        _mapperMock.Setup(m =>
            m.Map<GetByIdDoctorResponse>(repositoryResult))
            .Returns(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _doctorRepositoryMock.Verify(
            r => r.GetDoctorInfoById(id, It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.Equal(expectedResponse.Doctor.FirstName, result.Doctor.FirstName);
        Assert.Equal(expectedResponse.Doctor.LastName, result.Doctor.LastName);
        Assert.Equal(expectedResponse.Doctor.MiddleName, result.Doctor.MiddleName);
        Assert.Equal(expectedResponse.Doctor.Status, result.Doctor.Status);
        Assert.Equal(expectedResponse.Doctor.BirthDate, result.Doctor.BirthDate);
        Assert.Equal(expectedResponse.Doctor.CareerStartDay, result.Doctor.CareerStartDay);

    }
}
