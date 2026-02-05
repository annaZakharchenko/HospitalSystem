using HospitalSystem.Data;
using HospitalSystem.DTOs.Doctor;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class DoctorService : IDoctorService
{
    private readonly HospitalDbContext _context;

    public DoctorService(HospitalDbContext context)
    {
        _context = context;
    }
    

    public async Task<IEnumerable<DoctorDto>> GetAllAsync()
    {
        return await _context.Doctors
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                FirstName =  d.FirstName,
                LastName =  d.LastName,
                Specialization = d.Specialization
            })
            .ToListAsync();
    }
    
    public async Task<DoctorDto?> GetProfileAsync(int userId)
    {
        var doctor = await _context.Doctors
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId);
            
        if (doctor == null) return null;

        return new DoctorDto
        {
            Id = doctor.Id,
            Email = doctor.User.Email,
            FirstName =  doctor.FirstName,
            LastName =  doctor.LastName,
            Specialization = doctor.Specialization
        };
    }
    
    public async Task<DoctorDto> UpdateProfileAsync(int userId, UpdateDoctorDto dto)
    {
        var doctor = await _context.Doctors
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.UserId == userId);

        if (doctor == null)
            throw new Exception("Doctor not found");

        doctor.FirstName = dto.FirstName;
        doctor.LastName = dto.LastName;
        doctor.Specialization = dto.Specialization;

        await _context.SaveChangesAsync();
        
        return new DoctorDto()
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.User.Email,
            Specialization = doctor.Specialization
        };
    }

}