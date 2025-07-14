using AutoMapper;
using DoctorAPI.Application.Queries.Doctor.GetById;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using Moq;
using DoctorAPI.Application.Enums;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.Application.Contracts.Cache;

namespace DoctorAPI.UnitTests.Queries.Doctor.GetById;

public class GetDoctorByIdQueryHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly GetDoctorByIdQueryHandler _handler;

    public GetDoctorByIdQueryHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _handler = new GetDoctorByIdQueryHandler(
            _mapperMock.Object,
            _doctorRepositoryMock.Object,
            _cacheServiceMock.Object);
    }

    [Fact]
    public async Task Handle_WhenValidIdProvided_ShouldReturnDoctor()
    {
        // Arrange
        var id = Guid.NewGuid();
        var query = new GetDoctorByIdQuery(id);

        var repositoryResult = GetRepositoryResult(id);

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

        Assert.Equal(expectedResponse.Doctor.Id, result.Doctor.Id);
    }

    private GetDoctorInfoByIdResult GetRepositoryResult(Guid id)
    {
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

        return repositoryResult;
    }
}
