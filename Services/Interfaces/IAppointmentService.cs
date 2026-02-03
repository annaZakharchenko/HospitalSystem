using HospitalSystem.DTOs;

namespace HospitalSystem.Services.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentDto> CreateAsync(int patientId, CreateAppointmentDto dto);
    Task<IEnumerable<AppointmentDto>> GetAllAsync();
    Task<IEnumerable<AppointmentDto>> GetByDoctorIdAsync(int doctorId);
    Task<IEnumerable<AppointmentDto>> GetByPatientIdAsync(int patientId);
    
    Task<IEnumerable<AppointmentDto>> GetByDateAsync(DateTime date);

}