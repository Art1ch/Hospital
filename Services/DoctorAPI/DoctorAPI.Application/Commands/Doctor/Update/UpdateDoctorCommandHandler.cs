using AutoMapper;
using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Application.Entities;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Update;

internal class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateDoctorCommand command, CancellationToken cancellationToken)
    {
        var doctorEntity = _mapper.Map<DoctorEntity>(command.Request);
        await _unitOfWork.DoctorRepository.UpdateAsync(doctorEntity);   
    }
}
