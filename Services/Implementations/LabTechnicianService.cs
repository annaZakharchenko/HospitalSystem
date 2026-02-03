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

    public async Task UpdateProfileAsync(int userId, UpdateLabTechnicianDto dto)
    {
        var lab = await _context.LabTechnicians
            .FirstOrDefaultAsync(l => l.UserId == userId);

        if (lab == null)
            throw new Exception("Lab technician not found");

        lab.FirstName = dto.FirstName;
        lab.LastName = dto.LastName;

        await _context.SaveChangesAsync();
    }
}