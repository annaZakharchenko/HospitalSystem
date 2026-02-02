namespace HospitalSystem.Domain.Entities;

public class MedicalRecord
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<MedicalRecordEntry> Entries { get; set; } = new List<MedicalRecordEntry>();
    public ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();
}