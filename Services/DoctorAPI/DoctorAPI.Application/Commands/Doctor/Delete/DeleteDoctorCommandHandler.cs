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
        using (var unitOfWork = _unitOfWork)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await unitOfWork.DoctorRepository.DeleteAsync(command.Id, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
