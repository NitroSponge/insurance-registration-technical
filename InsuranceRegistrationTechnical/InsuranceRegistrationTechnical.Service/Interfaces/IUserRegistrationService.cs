using InsuranceRegistrationTechnical.Service.Models;
using InsuranceRegistrationTechnical.Service.Tools;

namespace InsuranceRegistrationTechnical.Service.Interfaces;
public interface IUserRegistrationService
{
    Task<ServiceResult<int>> RegisterUserAsync(RegisterUserRequestModel request, CancellationToken cancellationToken);
}