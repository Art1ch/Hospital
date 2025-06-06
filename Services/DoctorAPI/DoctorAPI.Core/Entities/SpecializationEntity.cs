namespace DoctorAPI.Core.Entities;

public class SpecializationEntity<TId1, TId2>
{
    public TId1 Id { get; set; }
    public string Name { get; set; }
    public TId2 DoctorId { get; set; }
    public DoctorEntity<TId1, TId2> Doctor { get; set; }
}
