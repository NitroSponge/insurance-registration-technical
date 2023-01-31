using InsuranceRegistrationTechnical.Data.Data;
using InsuranceRegistrationTechnical.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRegistrationTechnical.Data.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected RegistrationDatabaseContext DatabaseContext { get; }

    public RepositoryBase(RegistrationDatabaseContext databaseContext)
    {
        DatabaseContext = databaseContext;
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var entityEntry = await DatabaseContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await DatabaseContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task SaveAsync(CancellationToken cancellationToken)
        => await DatabaseContext.SaveChangesAsync(cancellationToken);
}
