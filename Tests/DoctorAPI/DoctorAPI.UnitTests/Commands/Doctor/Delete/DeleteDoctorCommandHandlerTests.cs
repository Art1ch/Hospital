using DoctorAPI.Application.Commands.Doctor.Delete;
using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using Moq;

namespace DoctorAPI.UnitTests.Commands.Doctor.Delete;

public class DeleteDoctorCommandHandlerTests
{
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly DeleteDoctorCommandHandler _handler;

    public DeleteDoctorCommandHandlerTests()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _handler = new DeleteDoctorCommandHandler(_doctorRepositoryMock.Object, _cacheServiceMock.Object);        
    }

    [Fact]
    public async Task Handle_WhenValidIdProvided_ShouldDeleteDoctor()
    {
        // Arrange
        var testId = Guid.NewGuid();
        var command = new DeleteDoctorCommand(testId);

        _doctorRepositoryMock.Setup(r => r.DeleteAsync(testId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _doctorRepositoryMock.Verify(
            r => r.DeleteAsync(testId, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}