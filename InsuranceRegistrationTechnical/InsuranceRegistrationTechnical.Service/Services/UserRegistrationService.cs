using InsuranceRegistrationTechnical.Data.Entities;
using InsuranceRegistrationTechnical.Data.Interfaces;
using InsuranceRegistrationTechnical.Service.Configuration;
using InsuranceRegistrationTechnical.Service.Framework;
using InsuranceRegistrationTechnical.Service.Interfaces;
using InsuranceRegistrationTechnical.Service.Models;
using InsuranceRegistrationTechnical.Service.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace InsuranceRegistrationTechnical.Service.Services;
public class UserRegistrationService : IUserRegistrationService
{
    private readonly ILogger<UserRegistrationService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IAgeRestrictionEvaluator _ageRestrictionEvaluator;
    private readonly UserRegistrationServiceOptions _options;
    private readonly Regex _validPolicyRegistrationNumberRegex;
    private readonly Regex _validEmailRegex;

    private const string ValidEmailRegexRaw = @"[a-zA-Z0-9]{3,}@[a-zA-Z0-9]{2,}(.com|.co.uk)";
    private const int RegexTimeoutSeconds = 1;

    public UserRegistrationService(
        ILogger<UserRegistrationService> logger,
        IUserRepository userRepository,
        IAgeRestrictionEvaluator ageRestrictionEvaluator,
        IOptions<UserRegistrationServiceOptions> options)
    {
        _logger = logger;
        _userRepository = userRepository;
        _ageRestrictionEvaluator = ageRestrictionEvaluator;
        _options = options.Value;
        var regexTimeoutTimeSpan = TimeSpan.FromSeconds(RegexTimeoutSeconds);
        _validPolicyRegistrationNumberRegex = new Regex(_options.PolicyReferenceRegex, RegexOptions.None, regexTimeoutTimeSpan);
        _validEmailRegex = new Regex(ValidEmailRegexRaw, RegexOptions.None, regexTimeoutTimeSpan);
    }

    public async Task<ServiceResult<int>> RegisterUserAsync(RegisterUserRequestModel request, CancellationToken cancellationToken)
    {
        ServiceResult<int> result;
        var validationResult = ValidateRegisterUserRequestModel(request);
        if (validationResult.IsValid)
        {
            result = await SaveNewUserAsync(new()
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                PolicyReferenceNumber = request.PolicyReferenceNumber,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email
            }, cancellationToken);
        }
        else
        {
            result = ServiceResult.FailureFromErrors<int>(validationResult.Errors);
        }
        
        return result;
    }

    private ValidationResult ValidateRegisterUserRequestModel(RegisterUserRequestModel request)
    {
        var validationResult = new ValidationResult();
        if (ValidateName(request.FirstName))
        {
            validationResult.AddError("First Name must be between 3 and 50 characters.");
        }
        if (ValidateName(request.Surname))
        {
            validationResult.AddError("Surname must be between 3 and 50 characters.");
        }
        if (request.PolicyReferenceNumber != null && _validPolicyRegistrationNumberRegex.IsMatch(request.PolicyReferenceNumber) == false)
        {
            validationResult.AddError("Policy Reference Number must be of form XX-000000 where XX is an uppercase alpha character and 0 is a digit.");
        }
        if (request.Email != null && _validEmailRegex.IsMatch(request.Email) == false)
        {
            validationResult.AddError("Email must have at least four alpha numeric characters before the @ sign, two alpha numeric characters after the @ sign excluding domain and must be of domain .com or .co.uk");
        }
        if (request.DateOfBirth != null && _ageRestrictionEvaluator.IsOverAgeRestriction(request.DateOfBirth.Value) == false)
        {
            validationResult.AddError("Customer must be over eighteen.");
        }
        if (request.DateOfBirth == null && request.Email == null)
        {
            validationResult.AddError("Either Date of Birth or Email must be provided.");
        }
        return validationResult;
    }

    private async Task<ServiceResult<int>> SaveNewUserAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        var savedUser = await _userRepository.CreateAsync(userEntity, cancellationToken);
        bool databaseUpdateSuccess;
        try
        {
            await _userRepository.SaveAsync(cancellationToken);
            databaseUpdateSuccess = true;
        }
        // DataAccessLayer bleeding here, would need to return custom errors from repositories to fix.
        catch (DbUpdateException)
        {
            databaseUpdateSuccess = false;
        }
        return databaseUpdateSuccess ?
            ServiceResult.SuccessFromValue(savedUser.Id)
            : ServiceResult.FailureFromError<int>("Database validation error, please contact administrator if persists.");
    }

    private bool ValidateName(string name)
        => name == null || name.Length < _options.MinimumNameLength || name.Length > _options.MaximumNameLength;
}
