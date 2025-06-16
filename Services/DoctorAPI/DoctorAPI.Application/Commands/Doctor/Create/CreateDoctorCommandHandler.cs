using AutoMapper;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Application.Entities;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

internal class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateDoctorCommand command, CancellationToken cancellationToken)
    {
        var doctorEntity = _mapper.Map<DoctorEntity>(command.Request);
        await _unitOfWork.DoctorRepository.CreateAsync(doctorEntity, cancellationToken);   
    }
}
