using AutoMapper;
using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;
using DoctorAPI.Application.Requests.Doctor;
using Moq;

namespace DoctorAPI.UnitTests.Commands.Doctor.Create;

public class CreateDoctorCommandHandlerTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock = new();
    private readonly CreateDoctorCommandHandler _handler;

    public CreateDoctorCommandHandlerTests()
    {
        _handler = new CreateDoctorCommandHandler(_mapperMock.Object, _doctorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Doctor_Successfully()
    {
        // Arrange
        var testBirthDate = new DateOnly(1990, 1, 1);
        var testCareerStartDate = new DateOnly(2015, 1, 1);

        var request = new CreateDoctorRequest(
            "TestFirstName",
            "TestMiddleName",
            "TestLastName",
            DoctorStatus.Available,
            testBirthDate,
            testCareerStartDate,
            "TestSpecializationName"
            );   

        var command = new CreateDoctorCommand(request);
        var expectedDoctor = new DoctorEntity
        {
            FirstName = request.FirstName,
            MiddleName = request.LastName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            CareerStartDay = request.CareerStartDay,
            Status = request.Status,
        };

        _mapperMock.Setup(m => m.Map<DoctorEntity>(request))
                 .Returns(expectedDoctor);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<DoctorEntity>(request), Times.Once);

        _doctorRepositoryMock.Verify(r => r.CreateAsync(
            It.Is<DoctorEntity>(d => 
                d.FirstName == expectedDoctor.FirstName &&
                d.MiddleName == expectedDoctor.MiddleName &&
                d.LastName == expectedDoctor.LastName &&
                d.Status == expectedDoctor.Status &&
                d.BirthDate == expectedDoctor.BirthDate &&
                d.CareerStartDay == expectedDoctor.CareerStartDay),                
            It.IsAny<CancellationToken>()),
            Times.Once);
    }
}