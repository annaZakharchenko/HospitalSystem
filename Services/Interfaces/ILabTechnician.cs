using HospitalSystem.DTOs.User.LabTechnician;

namespace HospitalSystem.Services.Interfaces;

public interface ILabTechnician
{
    Task<IEnumerable<LabTechnicianDto>> GetAllAsync();
    Task<LabTechnicianDto?> GetProfileAsync(int userId);  

    Task<LabTechnicianDto> UpdateProfileAsync(int userId, UpdateLabTechnicianDto dto);

}