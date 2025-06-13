using DoctorAPI.Application.Contracts.UnitOfWork;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

internal class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDoctorCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.DoctorRepository.DeleteAsync(command.Id);
    }
}
