using DoctorAPI.Application.Contracts.Repository.Doctor;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

internal class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand>
{
    private readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task Handle(DeleteDoctorCommand command, CancellationToken cancellationToken)
    {
        await _doctorRepository.DeleteAsync(command.Id);
    }
}
