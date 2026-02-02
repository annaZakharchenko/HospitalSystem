using HospitalSystem.Domain.Enums;

namespace HospitalSystem.Domain.Entities;

public class LabTest
{
    public int Id { get; set; }

    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public int PatientId { get; set; }
    public string TestType { get; set; } = null!;
    public LabTestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LabTestResult? Result { get; set; }
}