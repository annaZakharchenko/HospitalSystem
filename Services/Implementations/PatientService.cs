using HospitalSystem.Data;
using HospitalSystem.Domain.Entities;
using HospitalSystem.DTOs.Patient;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Domain.Enums;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly HospitalDbContext _context;

    public PatientService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = "TEMP_HASH", // потом будет auth
            Role = UserRole.Patient
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var patient = new Patient
        {
            UserId = user.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Phone = dto.Phone
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        _context.MedicalRecords.Add(new MedicalRecord
        {
            PatientId = patient.Id
        });

        await _context.SaveChangesAsync();

        return new PatientDto
        {
            Id = patient.Id,
            FullName = $"{patient.FirstName} {patient.LastName}",
            Email = user.Email
        };
    }


    public async Task<IEnumerable<PatientDto>> GetAllAsync()
    {
        return await _context.Patients
            .Select(p => new PatientDto
            {
                Id = p.Id,
                FullName = p.FirstName + " " + p.LastName,
                Email = p.User.Email
            })
            .ToListAsync();
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return null;

        return new PatientDto
        {
            Id = patient.Id,
            FullName = patient.FirstName + " " + patient.LastName,
            Email = patient.User.Email
        };
    }
}