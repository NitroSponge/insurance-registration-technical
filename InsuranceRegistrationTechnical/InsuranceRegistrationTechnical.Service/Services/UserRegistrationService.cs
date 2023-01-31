using InsuranceRegistrationTechnical.Data.Interfaces;
using InsuranceRegistrationTechnical.Service.Interfaces;
using InsuranceRegistrationTechnical.Service.Models;
using Microsoft.Extensions.Logging;

namespace InsuranceRegistrationTechnical.Service.Services;
public class UserRegistrationService : IUserRegistrationService
{
    private readonly ILogger<UserRegistrationService> _logger;
    private readonly IUserRepository _userRepository;

    public UserRegistrationService(ILogger<UserRegistrationService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<int> RegisterUserAsync(RegisterUserRequestModel request, CancellationToken cancellationToken)
    {
        await _userRepository.CreateAsync(new()
        {
            FirstName = request.FirstName,
            Surname = request.Surname,
            PolicyReferenceNumber = request.PolicyReferenceNumber
        }, cancellationToken);
        await _userRepository.SaveAsync(cancellationToken);
        var savedUsers = await _userRepository.GetAllAsync(cancellationToken);
        return savedUsers.Count();
    }
}
