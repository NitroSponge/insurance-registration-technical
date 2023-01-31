using InsuranceRegistrationTechnical.Service.Models;

namespace InsuranceRegistrationTechnical.Service.Interfaces;
public interface IUserRegistrationService
{
    Task<int?> RegisterUserAsync(RegisterUserRequestModel request, CancellationToken cancellationToken);
}