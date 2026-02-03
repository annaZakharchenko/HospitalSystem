using HospitalSystem.Domain.Entities;
using HospitalSystem.Domain.Enums;

namespace HospitalSystem.Data;

public static class DbInitializer
{
    public static async Task SeedAdminAsync(HospitalDbContext context)
    {
        if (context.Users.Any(u => u.Role == UserRole.Admin))
            return;

        var admin = new User
        {
            Email = "admin@hospital.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminHospital26"),
            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(admin);
        await context.SaveChangesAsync();

        Console.WriteLine("Admin user created!");
        }
}