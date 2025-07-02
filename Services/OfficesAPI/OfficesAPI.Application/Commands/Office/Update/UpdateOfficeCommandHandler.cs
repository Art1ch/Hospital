using AutoMapper;
using MediatR;
using OfficesAPI.Application.Contracts.Repository.Office;
using OfficesAPI.Core.Entities;

namespace OfficesAPI.Application.Commands.Office.Update;

internal sealed class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Unit>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;

    public UpdateOfficeCommandHandler(
        IOfficeRepository officeRepository,
        IMapper mapper)
    {
        _officeRepository = officeRepository;
        _mapper = mapper;
    }


    public async Task<Unit> Handle(UpdateOfficeCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var officeEntity = _mapper.Map<OfficeEntity>(request);
        await _officeRepository.UpdateAsync(officeEntity, cancellationToken);
        return Unit.Value;
    }
}   
