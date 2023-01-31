using InsuranceRegistrationTechnical.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRegistrationTechnical.Data.Data;

public class RegistrationDatabaseContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
}