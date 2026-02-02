using HospitalSystem.DTOs.MedicalRecord;

namespace HospitalSystem.Services.Interfaces;

public interface IMedicalRecordService
{
    Task<MedicalRecordDto> GetByPatientIdAsync(int patientId);
}