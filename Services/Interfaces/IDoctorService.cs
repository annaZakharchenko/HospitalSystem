using HospitalSystem.DTOs.Doctor;

namespace HospitalSystem.Services.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync();
    Task UpdateProfileAsync(int userId, UpdateDoctorDto dto);

}