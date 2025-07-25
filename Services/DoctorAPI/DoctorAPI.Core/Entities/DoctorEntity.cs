using DoctorAPI.Application.Enums;

namespace DoctorAPI.Application.Entities;

public class DoctorEntity
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DoctorStatus Status { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly CareerStartDay { get; set; }
    public int SpecializationId { get; set; }
    public SpecializationEntity Specialization { get; set; }
}
