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

        var repositoryResults = CreateListOfGetAllDoctorsResult(pageSize);
        
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

        for (int i = 0; i < repositoryResults.Count; i++)
        {
            Assert.Equal(expectedResponse.Doctors[i].Id, result.Doctors[i].Id);
        }
    }

    private List<GetAllDoctorsResult> CreateListOfGetAllDoctorsResult(int pageSize)
    {
        var repositoryResults = new List<GetAllDoctorsResult>();
        for (var i = 0; i < pageSize; i++)
        {
            repositoryResults.Add(
                new(Guid.NewGuid(), $"TestFirstName{i}", $"TestLastName{i}", $"TestMiddleName{i}"));
        }

        return repositoryResults;
    }
}