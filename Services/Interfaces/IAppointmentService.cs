using HospitalSystem.DTOs;

namespace HospitalSystem.Services.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);
    Task<IEnumerable<AppointmentDto>> GetAllAsync();
}