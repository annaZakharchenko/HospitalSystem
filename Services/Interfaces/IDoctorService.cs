using HospitalSystem.DTOs.Doctor;

namespace HospitalSystem.Services.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync();
}