using HospitalSystem.Domain.Enums;

namespace HospitalSystem.DTOs.LabTest;

public class LabTestDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientFullName { get; set; } = null!;

    public int DoctorId { get; set; }
    public string DoctorFullName { get; set; } = null!;

    public string TestType { get; set; } = null!;
    public LabTestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int? LabTechnicianId { get; set; }
    public string? LabTechnicianFullName { get; set; }

    public string? ResultValue { get; set; }
    public DateTime? ResultDate { get; set; }
}