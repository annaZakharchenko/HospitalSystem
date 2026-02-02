namespace HospitalSystem.DTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public string PatientName { get; set; } = null!;
    public string DoctorName { get; set; } = null!;
    public DateTime AppointmentDate { get; set; }
}