using AutoMapper;
using MediatR;
using OfficesAPI.Application.Contracts.Repository.Office;
using OfficesAPI.Core.Entities;

namespace OfficesAPI.Application.Commands.Office.Create;

internal sealed class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Unit>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public CreateOfficeCommandHandler(
        IOfficeRepository officeRepository,
        IMapper mapper)
    {
        _officeRepository = officeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateOfficeCommand command, CancellationToken cancellationToken)
    {
        var officeEntity = _mapper.Map<OfficeEntity>(command.Request);
        await _officeRepository.CreateAsync(officeEntity, cancellationToken);
        return Unit.Value;
    }
}
