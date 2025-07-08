using AutoMapper;
using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Commands.Doctor.Update;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;
using DoctorAPI.Application.Requests.Doctor;
using Moq;

namespace DoctorAPI.UnitTests.Commands.Doctor.Update;

public class UpdateDoctorCommandHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
    private readonly UpdateDoctorCommandHandler _handler;

    public UpdateDoctorCommandHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _handler = new UpdateDoctorCommandHandler(_mapperMock.Object, _doctorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenValidDataProvided_ShouldUpdateDoctor()
    {
        // Arrange
        var testBirthDate = new DateOnly(1990, 1, 1);
        var testCareerStartDate = new DateOnly(2015, 1, 1);

        var request = new UpdateDoctorRequest(
        
            Guid.NewGuid(),
            "TestFirstName",
            "TestLastName",
            "TestMiddleName",
            DoctorStatus.Available,
            testBirthDate,
            testCareerStartDate,
            "TestSpecializationName"
        );

        var command = new UpdateDoctorCommand(request);

        var expectedDoctor = GetEntity(request);

        _mapperMock.Setup(m => m.Map<DoctorEntity>(request))
            .Returns(expectedDoctor);

        _doctorRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<DoctorEntity>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<DoctorEntity>(request), Times.Once);

        _doctorRepositoryMock.Verify(r => r.UpdateAsync(
            It.Is<DoctorEntity>(d =>
                d.Id == expectedDoctor.Id &&
                d.FirstName == expectedDoctor.FirstName &&
                d.LastName == expectedDoctor.LastName &&
                d.MiddleName == expectedDoctor.MiddleName &&
                d.Status == expectedDoctor.Status &&
                d.BirthDate == expectedDoctor.BirthDate &&
                d.CareerStartDay == expectedDoctor.CareerStartDay),
            It.IsAny<CancellationToken>()),
            Times.Once
        );

    }

    private DoctorEntity GetEntity(UpdateDoctorRequest request)
    {
        var expectedDoctor = new DoctorEntity
        {
            Id = request.Id,
            FirstName = request.FirstName,
            MiddleName = request.LastName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            CareerStartDay = request.CareerStartDay,
            Status = request.Status,
        };

        return expectedDoctor;
    }
}

