namespace HospitalSystem.DTOs.MedicalRecord;

public class MedicalRecordDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateTime Date { get; set; }
    public List<CreateMedicalRecordEntryDto> Entries { get; set; } = new();
}