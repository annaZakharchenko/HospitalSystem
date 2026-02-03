using HospitalSystem.DTOs.User.LabTechnician;

namespace HospitalSystem.Services.Interfaces;

public interface ILabTechnician
{
    Task<IEnumerable<LabTechnicianDto>> GetAllAsync();
    Task UpdateProfileAsync(int userId, UpdateLabTechnicianDto dto);

}