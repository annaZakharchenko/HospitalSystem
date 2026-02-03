using HospitalSystem.DTOs.Admin;

namespace HospitalSystem.Services.Interfaces;

public interface IAdminService
{
    Task CreateDoctorAsync(CreateStaffDto dto);
    Task CreateLabTechnicianAsync(CreateStaffDto dto);
}