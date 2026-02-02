namespace HospitalSystem.Domain.Entities;

public class MedicalRecordEntry
{
    public int Id { get; set; }

    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public string EntryType { get; set; } = null!;
    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}