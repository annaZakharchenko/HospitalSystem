using HospitalSystem.DTOs.Doctor;

namespace HospitalSystem.Services.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync();
    Task<DoctorDto?> GetProfileAsync(int userId);  

    Task<DoctorDto> UpdateProfileAsync(int userId, UpdateDoctorDto dto);

}