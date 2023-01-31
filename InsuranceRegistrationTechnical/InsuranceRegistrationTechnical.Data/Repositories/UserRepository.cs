using InsuranceRegistrationTechnical.Data.Data;
using InsuranceRegistrationTechnical.Data.Entities;
using InsuranceRegistrationTechnical.Data.Interfaces;

namespace InsuranceRegistrationTechnical.Data.Repositories;
public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
{
    public UserRepository(RegistrationDatabaseContext databaseContext) : base(databaseContext)
    {
    }
}
