using MediatR;
using OfficesAPI.Application.Contracts.Repository.Office;

namespace OfficesAPI.Application.Commands.Office.Delete;

internal sealed class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand, Unit>
{
    private readonly IOfficeRepository _officeRepository;

    public DeleteOfficeCommandHandler(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<Unit> Handle(DeleteOfficeCommand command, CancellationToken cancellationToken)
    {
        await _officeRepository.DeleteAsync(command.Id, cancellationToken);
        return Unit.Value;
    }
}
