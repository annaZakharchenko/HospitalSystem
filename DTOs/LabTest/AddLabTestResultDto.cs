namespace HospitalSystem.DTOs.LabTest;

public class AddLabTestResultDto
{
    public int LabTestId { get; set; }
    public int LabTechnicianId { get; set; }
    public string Result { get; set; } = null!;
}