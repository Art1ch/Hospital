using MediatR;
using OfficesAPI.Application.Contracts.Repository.Office;

namespace OfficesAPI.Application.Commands.Office.ChangeStatus;

internal sealed class ChangeOfficeStatusCommandHandler : IRequestHandler<ChangeOfficeStatusCommand, Unit>
{
    private readonly IOfficeRepository _officeRepository;

    public ChangeOfficeStatusCommandHandler(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<Unit> Handle(ChangeOfficeStatusCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        await _officeRepository.ChangeOfficeStatusAsync(request.Id, request.Status, cancellationToken);
        return Unit.Value;
    }
}
