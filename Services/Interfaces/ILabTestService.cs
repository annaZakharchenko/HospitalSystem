using HospitalSystem.DTOs.LabTest;

namespace HospitalSystem.Services.Interfaces;

public interface ILabTestService
{
    Task<LabTestDto> CreateAsync(CreateLabTestDto dto);
    Task<LabTestDto> AddResultAsync(AddLabTestResultDto dto);  // ✅ Добавить результат
    Task<IEnumerable<LabTestDto>> GetByPatientIdAsync(int patientId);
    Task<IEnumerable<LabTestDto>> SearchByPatientNameAsync(string patientName);  // ✅ Поиск по имени
    Task<LabTestDto> GetByIdAsync(int id);
    
}