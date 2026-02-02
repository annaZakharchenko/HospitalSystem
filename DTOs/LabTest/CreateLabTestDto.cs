namespace HospitalSystem.DTOs.LabTest;

public class CreateLabTestDto
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int MedicalRecordId { get; set; } 
    public string TestType { get; set; }
}