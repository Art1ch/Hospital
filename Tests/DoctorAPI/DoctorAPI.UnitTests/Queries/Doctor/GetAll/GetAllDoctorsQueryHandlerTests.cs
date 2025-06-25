using AutoMapper;
using DoctorAPI.Application.Queries.Doctor.GetAll;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using Moq;
using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.Responses.Doctor;

namespace DoctorAPI.UnitTests.Queries.Doctor.GetAll;

public class GetAllDoctorsQueryHandlerTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock = new();
    private readonly GetAllDoctorsQueryHandler _handler;

    public GetAllDoctorsQueryHandlerTests()
    {
        _handler = new GetAllDoctorsQueryHandler(_mapperMock.Object, _doctorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Doctors_From_Repository()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;
        var query = new GetAllDoctorsQuery(page, pageSize);
        var repositoryResults = new List<GetAllDoctorsResult>();
        for (int i = 0; i < pageSize; i++)
        {
            repositoryResults.Add(
                new(Guid.NewGuid(), $"TestFirstName{i}", $"TestLastName{i}", $"TestMiddleName{i}"));
        }
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
        Assert.Equal(repositoryResults[0].FirstName, result.Doctors[0].FirstName);
        Assert.Equal(repositoryResults[0].LastName, result.Doctors[0].LastName);
        Assert.Equal(repositoryResults[0].MiddleName, result.Doctors[0].MiddleName);
    }

    [Fact]
    public async Task Handle_Should_Return_Empty_List_When_No_Doctors()
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