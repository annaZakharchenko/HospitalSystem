namespace HospitalSystem.DTOs.MedicalRecord;

public class MedicalRecordDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateTime Date { get; set; }
    public List<MedicalRecordEntryDto> Entries { get; set; } = new();
}