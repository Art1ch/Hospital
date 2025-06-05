namespace DoctorAPI.Core.Entities;

public class SpecializationEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DoctorId { get; set; }
    public DoctorEntity Doctor { get; set; }
}
