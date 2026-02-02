using HospitalSystem.DTOs.Patient;

namespace HospitalSystem.Services.Interfaces;

public interface IPatientService
{
    Task<PatientDto> CreateAsync(CreatePatientDto dto);
    Task<IEnumerable<PatientDto>> GetAllAsync();
    Task<PatientDto?> GetByIdAsync(int id);
}