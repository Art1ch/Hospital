namespace DoctorAPI.Core.Entities;

public class SpecializationEntity<TDoctorId, TSpecializationId>
{
    public TSpecializationId Id { get; set; }
    public string Name { get; set; }
    public TDoctorId DoctorId { get; set; }
    public DoctorEntity<TDoctorId, TSpecializationId> Doctor { get; set; }
}
