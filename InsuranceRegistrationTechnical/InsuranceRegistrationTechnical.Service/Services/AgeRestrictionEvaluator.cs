using InsuranceRegistrationTechnical.Service.Configuration;
using InsuranceRegistrationTechnical.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace InsuranceRegistrationTechnical.Service.Services;
public class AgeRestrictionEvaluator : IAgeRestrictionEvaluator
{
    private readonly UserRegistrationServiceOptions _options;

    public AgeRestrictionEvaluator(IOptions<UserRegistrationServiceOptions> options)
    {
        _options = options.Value;
    }

    // This needs work. There are lots of gotchas with dates that I have not taken the time to consider.
    public bool IsOverAgeRestriction(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var ageRestrictionMinimum = today.AddYears(0 - _options.AgeRestrictionYears);
        return dateOfBirth <= ageRestrictionMinimum;
    }
}
