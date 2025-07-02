using AutoMapper;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Enums;
using DoctorAPI.Application.Queries.Doctor.GetByStatus;
using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;
using DoctorAPI.Application.Responses.Doctor;
using Moq;

namespace DoctorAPI.UnitTests.Queries.Doctor.GetByStatus;

public class GetDoctorsByStatusQueryHandlerTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock = new();
    private readonly GetDoctorsByStatusQueryHandler _handler;

    public GetDoctorsByStatusQueryHandlerTests()
    {
        _handler = new GetDoctorsByStatusQueryHandler(_doctorRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenValidStatusProvided_ShouldReturnDoctor()
    {
        // Arrange
        var status = DoctorStatus.Available;
        var query = new GetDoctorsByStatusQuery(status);
        var doctorsCount = 5;

        var repositoryResults = new List<GetDoctorsByStatusResult>();

        for (int i = 0; i < doctorsCount; i++)
        {
            repositoryResults.Add(
                new(Guid.NewGuid(), $"TestFirstName{i}", $"TestLastName{i}", $"TestMiddleName{i}"));
        }

        var expectedResponse = new GetByStatusDoctorsResponse(repositoryResults);

        _doctorRepositoryMock.Setup(r =>
            r.GetDoctorByStatusAsync(status, It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryResults);

        _mapperMock.Setup(m =>
            m.Map<GetByStatusDoctorsResponse>(repositoryResults))
            .Returns(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _doctorRepositoryMock.Verify(
            r => r.GetDoctorByStatusAsync(status, It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.Equal(doctorsCount, result.Doctors.Count);
        Assert.Equal(expectedResponse.Doctors[0].FirstName, result.Doctors[0].FirstName);
        Assert.Equal(expectedResponse.Doctors[0].LastName, result.Doctors[0].LastName);
        Assert.Equal(expectedResponse.Doctors[0].MiddleName, result.Doctors[0].MiddleName);
    }

    [Fact]
    public async Task Handle_WhenValidStatusProvided_ShouldReturnEmptyListOfDoctors()
    {
        // Arrange
        var status = DoctorStatus.OnVacation;
        var query = new GetDoctorsByStatusQuery(status);
        var emptyResults = new List<GetDoctorsByStatusResult>();

        _doctorRepositoryMock.Setup(r =>
            r.GetDoctorByStatusAsync(status, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyResults);

        _mapperMock.Setup(m =>
            m.Map<GetByStatusDoctorsResponse>(emptyResults))
            .Returns(new GetByStatusDoctorsResponse(emptyResults));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result.Doctors);
    }
}
