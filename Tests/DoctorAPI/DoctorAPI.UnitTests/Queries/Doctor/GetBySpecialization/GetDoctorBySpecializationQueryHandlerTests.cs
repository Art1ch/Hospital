using AutoMapper;
using DoctorAPI.Application.Queries.Doctor.GetBySpecialization;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using Moq;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.Application.Contracts.Cache;

namespace DoctorAPI.UnitTests.Queries.Doctor.GetBySpecialization;

public class GetDoctorBySpecializationQueryHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly GetDoctorBySpecializationQueryHandler _handler;

    public GetDoctorBySpecializationQueryHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _handler = new GetDoctorBySpecializationQueryHandler(
            _mapperMock.Object,
            _doctorRepositoryMock.Object,
            _cacheServiceMock.Object);
    }

    [Fact]
    public async Task Handle_WhenValidSpecializationIdProvided_ShouldReturnDoctor()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var specializationId = 1;
        var query = new GetDoctorBySpecializationQuery(specializationId);

        var repositoryResult = CreateObjectOfGetDoctorBySpecializationResult(doctorId, specializationId);
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
    }

    private GetDoctorBySpecializationResult CreateObjectOfGetDoctorBySpecializationResult(Guid doctorId, int specializationId)
    {
        var query = new GetDoctorBySpecializationQuery(specializationId);

        var repositoryResult = new GetDoctorBySpecializationResult(
            Id: doctorId,
            FirstName: "TestFirstName",
            LastName: "TestLastName",
            MiddleName: "TestMiddleName");

        return repositoryResult;
    }
}
