using AutoMapper;
using DoctorAPI.Application.Queries.Doctor.GetBySpecialization;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using Moq;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.Responses.Doctor;

namespace DoctorAPI.UnitTests.Queries.Doctor.GetBySpecialization;

public class GetDoctorBySpecializationQueryHandlerTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock = new();
    private readonly GetDoctorBySpecializationQueryHandler _handler;

    public GetDoctorBySpecializationQueryHandlerTests()
    {
        _handler = new GetDoctorBySpecializationQueryHandler(
            _mapperMock.Object,
            _doctorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenValidSpecializationIdProvided_ShouldReturnDoctor()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var specializationId = 1;
        var query = new GetDoctorBySpecializationQuery(specializationId);

        var repositoryResult = new GetDoctorBySpecializationResult(
            Id: doctorId,
            FirstName: "TestFirstName",
            LastName: "TestLastName",
            MiddleName: "TestMiddleName");

        var expectedResponse = new GetBySpecializationDoctorResponse(repositoryResult);

        _doctorRepositoryMock.Setup(r =>
            r.GetDoctorBySpecializationAsync(specializationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryResult);

        _mapperMock.Setup(m =>
            m.Map<GetBySpecializationDoctorResponse>(repositoryResult))
            .Returns(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _doctorRepositoryMock.Verify(
            r => r.GetDoctorBySpecializationAsync(specializationId, It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.Equal(doctorId, result.Doctor.Id);
        Assert.Equal(expectedResponse.Doctor.FirstName, result.Doctor.FirstName);
        Assert.Equal(expectedResponse.Doctor.LastName, result.Doctor.LastName);
        Assert.Equal(expectedResponse.Doctor.MiddleName, result.Doctor.MiddleName);
    }
}
