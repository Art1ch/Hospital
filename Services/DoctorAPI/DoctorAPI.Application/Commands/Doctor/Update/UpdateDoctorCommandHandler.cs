using AutoMapper;
using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Core.Entities;
using FluentValidation;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Update;

public class UpdateDoctorCommandHandler<TId1, TId2>
    : IRequestHandler<UpdateDoctorCommand<TId1, TId2>, UpdateDoctorResponseDto<TId1>>
{
    private readonly IDoctorRepository<TId1, TId2> _doctorRepository;
    private readonly IValidator<DoctorEntity<TId1, TId2>> _validator;
    private readonly IMapper _mapper;
    public UpdateDoctorCommandHandler
        (IDoctorRepository<TId1, TId2> doctorRepository,
        IValidator<DoctorEntity<TId1, TId2>> validator,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<UpdateDoctorResponseDto<TId1>> Handle(
        UpdateDoctorCommand<TId1, TId2> request,
        CancellationToken ct)
    {
        var doctorEntity = _mapper.Map<DoctorEntity<TId1, TId2>>(request.Dto);
        var validationResult = await _validator.ValidateAsync(doctorEntity);
        if (!validationResult.IsValid)
        {
            throw new Exception();
        }
        await _doctorRepository.Update(doctorEntity, ct);
        return _mapper.Map<UpdateDoctorResponseDto<TId1>>(doctorEntity);
    }
}
