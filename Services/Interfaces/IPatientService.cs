using HospitalSystem.DTOs.Patient;

namespace HospitalSystem.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAllAsync();
    Task<PatientDto?> GetByIdAsync(int id);
    Task<PatientDto?> GetProfileAsync(int userId);  
    Task<PatientDto> UpdateProfileAsync(int userId, UpdatePatientDto dto);
}