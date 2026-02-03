namespace HospitalSystem.DTOs.MedicalRecord;

public class CreateMedicalRecordEntryDto
{
    public int MedicalRecordId { get; set; }
    public string EntryType { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Description { get; set; } = null!;
}