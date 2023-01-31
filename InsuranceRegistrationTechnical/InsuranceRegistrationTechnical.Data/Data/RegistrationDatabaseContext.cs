using InsuranceRegistrationTechnical.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRegistrationTechnical.Data.Data;

public class RegistrationDatabaseContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public string DatabasePath { get; }

    private const string DatabaseName = "InsuranceRegistration.db";

    public RegistrationDatabaseContext()
        : base()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DatabasePath = Path.Join(path, DatabaseName);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
    }
}