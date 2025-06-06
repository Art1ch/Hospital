using AutoMapper;
using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Core.Entities;
using FluentValidation;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Update;

public class UpdateDoctorCommandHandler<TDoctorId,
    TSpecializationId>
    : IRequestHandler<UpdateDoctorCommand<TDoctorId,
        TSpecializationId>, UpdateDoctorResponseDto<TDoctorId>>
{
    private readonly IDoctorRepository<TDoctorId,
        TSpecializationId> _doctorRepository;
    private readonly IValidator<DoctorEntity<TDoctorId,
        TSpecializationId>> _validator;
    private readonly IMapper _mapper;
    public UpdateDoctorCommandHandler
        (IDoctorRepository<TDoctorId, TSpecializationId> doctorRepository,
        IValidator<DoctorEntity<TDoctorId, TSpecializationId>> validator,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<UpdateDoctorResponseDto<TDoctorId>> Handle(
        UpdateDoctorCommand<TDoctorId, TSpecializationId> request,
        CancellationToken ct)
    {
        var doctorEntity = _mapper.Map<DoctorEntity<TDoctorId, TSpecializationId>>(request.Dto);
        var validationResult = await _validator.ValidateAsync(doctorEntity);
        if (!validationResult.IsValid)
        {
            throw new Exception();
        }
        await _doctorRepository.UpdateAsync(doctorEntity, ct);
        return _mapper.Map<UpdateDoctorResponseDto<TDoctorId>>(doctorEntity);
    }
}
