using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalSystem.Data;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Domain.Enums;
using HospitalSystem.DTOs.Auth;
using HospitalSystem.DTOs.Patient;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



public class AuthService : IAuthService
{
    private readonly HospitalDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(HospitalDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    // {
    //     if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
    //         throw new Exception("User already exists");
    //
    //     var user = new User
    //     {
    //         Email = dto.Email,
    //         PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
    //         Role = dto.Role
    //     };
    //
    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();
    //
    //     return GenerateAuthResponse(user);
    // }
  
    public async Task<AuthResponseDto> RegisterPatientAsync(RegisterPatientDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("User already exists");

        var user = new User
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = UserRole.Patient
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // 🔹 создаём пациента
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

        return GenerateAuthResponse(user);
    }
    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return GenerateAuthResponse(user);
    }

    private AuthResponseDto GenerateAuthResponse(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email,
            Role = user.Role
        };
    }
}
