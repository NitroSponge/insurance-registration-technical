using InsuranceRegistrationTechnical.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InsuranceRegistrationTechnical.Data.Data;

public class RegistrationDatabaseContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public string DatabasePath { get; }

    public const string DatabaseNameOptionKey = "DatabaseName";

    public RegistrationDatabaseContext(IConfiguration configuration)
        : base()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DatabasePath = Path.Join(path, configuration.GetConnectionString(DatabaseNameOptionKey));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
    }
}