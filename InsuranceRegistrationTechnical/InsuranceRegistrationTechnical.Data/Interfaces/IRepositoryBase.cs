namespace InsuranceRegistrationTechnical.Data.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}