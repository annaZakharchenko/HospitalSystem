namespace HospitalSystem.Domain.Entities;

public class LabTestResult
{
    public int Id { get; set; }

    public int LabTestId { get; set; }
    public int  DoctorId { get; set; }
    public int PatientId { get; set; }
    public LabTest LabTest { get; set; } = null!;

    public int LabTechnicianId { get; set; }
    public LabTechnician LabTechnician { get; set; } = null!;

    public string Result { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}