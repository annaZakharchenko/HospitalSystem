using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalSystem.Data;

public class HospitalDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
{
    public HospitalDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseMySQL(connectionString!); 

        return new HospitalDbContext(optionsBuilder.Options);
    }
}