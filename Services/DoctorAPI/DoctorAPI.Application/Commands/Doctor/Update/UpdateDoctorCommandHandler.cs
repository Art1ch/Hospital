using AutoMapper;
using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Update;

internal class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;

    public UpdateDoctorCommandHandler(IMapper mapper, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
    }

    public async Task Handle(UpdateDoctorCommand command, CancellationToken cancellationToken)
    {
        var doctorEntity = _mapper.Map<DoctorEntity>(command.Request);
        await _doctorRepository.UpdateAsync(doctorEntity);   
    }
}
