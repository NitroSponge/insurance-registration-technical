using InsuranceRegistrationTechnical.Data.Interfaces;
using InsuranceRegistrationTechnical.Service.Interfaces;
using InsuranceRegistrationTechnical.Service.Models;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace InsuranceRegistrationTechnical.Service.Services;
public class UserRegistrationService : IUserRegistrationService
{
    private readonly ILogger<UserRegistrationService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly Regex _validPolicyRegistrationNumberRegex;
    private readonly Regex _validEmailRegex;

    public UserRegistrationService(ILogger<UserRegistrationService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _validPolicyRegistrationNumberRegex = new Regex(@"[A-Z]{2}-[\d]{6}");
        _validEmailRegex = new Regex(@"[a-zA-Z0-9]{3,}@[a-zA-Z0-9]{2,}(.com|.co.uk)");
    }

    public async Task<int?> RegisterUserAsync(RegisterUserRequestModel request, CancellationToken cancellationToken)
    {
        int? result;
        var isValid = ValidateRegisterUserRequestModel(request);
        if (isValid)
        {
            var savedUser = await _userRepository.CreateAsync(new()
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                PolicyReferenceNumber = request.PolicyReferenceNumber
            }, cancellationToken);
            await _userRepository.SaveAsync(cancellationToken);
            result = savedUser.Id;
        }
        else
        {
            result = 0;
        }
        return result;
    }

    private bool ValidateRegisterUserRequestModel(RegisterUserRequestModel request)
    {
        var isValid = true;
        if (ValidateName(request.FirstName))
        {
            isValid = false;
        }
        if (ValidateName(request.Surname))
        {
            isValid = false;
        }
        if (request.PolicyReferenceNumber != null && _validPolicyRegistrationNumberRegex.IsMatch(request.PolicyReferenceNumber) == false)
        {
            isValid = false;
        }
        if (request.Email != null && _validEmailRegex.IsMatch(request.Email) == false)
        {
            isValid = false;
        }
        if (request.PolicyReferenceNumber == null && request.Email == null)
        {
            isValid = false;
        }
        return isValid;
    }

    private static bool ValidateName(string name)
        => name == null || name.Length < 3 || name.Length > 50;
}
