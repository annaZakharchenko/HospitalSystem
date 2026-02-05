using HospitalSystem.Data;
using HospitalSystem.DTOs.User.LabTechnician;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class LabTechnicianService : ILabTechnician
{
    private readonly HospitalDbContext _context;

    public LabTechnicianService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LabTechnicianDto>> GetAllAsync()
    {
        return await _context.LabTechnicians
            .Select(l => new LabTechnicianDto
            {
                Id = l.Id,
                FirstName = l.FirstName,
                LastName = l.LastName
            })
            .ToListAsync();
    }

    public async Task<LabTechnicianDto?> GetProfileAsync(int userId)
    {
        var lab = await _context.LabTechnicians
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId);
            
        if (lab == null) return null;

        return new LabTechnicianDto()
        {
            Id = lab.Id,
            Email = lab.User.Email,
            FirstName = lab.FirstName,
            LastName = lab.LastName
        };
    }
    
    public async Task<LabTechnicianDto> UpdateProfileAsync(int userId, UpdateLabTechnicianDto dto)
    {
        var lab = await _context.LabTechnicians
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.UserId == userId);

        if (lab == null)
            throw new Exception("Lab technician not found");

        lab.FirstName = dto.FirstName;
        lab.LastName = dto.LastName;

        await _context.SaveChangesAsync();
        
        return new LabTechnicianDto
        {
            Id = lab.Id,
            Email = lab.User.Email,
            FirstName = lab.FirstName,
            LastName = lab.LastName
        };
    }
}