using AutoMapper;
using DoctorAPI.Application.Queries.Doctor.GetAll;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using Moq;
using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.Responses.Doctor;

namespace DoctorAPI.UnitTests.Queries.Doctor.GetAll;

public class GetAllDoctorsQueryHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly GetAllDoctorsQueryHandler _handler;

    public GetAllDoctorsQueryHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _handler = new GetAllDoctorsQueryHandler(_mapperMock.Object, _doctorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenValidDataProvided_ShouldReturnListOfDoctors()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;
        var query = new GetAllDoctorsQuery(page, pageSize);

        var repositoryResults = GetRepositoryResults(pageSize);
        
        var expectedResponse = new GetAllDoctorsResponse(repositoryResults);

        _doctorRepositoryMock.Setup(r =>
            r.GetAllDoctorsAsync(query.Page, query.PageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(repositoryResults);

        _mapperMock.Setup(m =>
            m.Map<GetAllDoctorsResponse>(repositoryResults))
            .Returns(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _doctorRepositoryMock.Verify(
            r => r.GetAllDoctorsAsync(query.Page, query.PageSize, It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.Equal(repositoryResults.Count, result.Doctors.Count);
    }

    private List<GetAllDoctorsResult> GetRepositoryResults(int pageSize)
    {
        var repositoryResults = new List<GetAllDoctorsResult>();
        for (var i = 0; i < pageSize; i++)
        {
            repositoryResults.Add(
                new(Guid.NewGuid(), $"TestFirstName{i}", $"TestLastName{i}", $"TestMiddleName{i}"));
        }

        return repositoryResults;
    }

    [Fact]
    public async Task Handle_WhenValidDataProvided_ShouldReturnEmptyListOfDoctors()
    {
        // Arrange
        var query = new GetAllDoctorsQuery(1, 10);
        var emptyResults = new List<GetAllDoctorsResult>();
        var expectedResponse = new GetAllDoctorsResponse(emptyResults);

        _doctorRepositoryMock.Setup(r =>
            r.GetAllDoctorsAsync(query.Page, query.PageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyResults);

        _mapperMock.Setup(m =>
            m.Map<GetAllDoctorsResponse>(emptyResults))
            .Returns(expectedResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result.Doctors);
    }
}