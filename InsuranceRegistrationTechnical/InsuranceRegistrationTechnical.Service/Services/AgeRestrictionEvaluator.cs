using InsuranceRegistrationTechnical.Service.Configuration;
using InsuranceRegistrationTechnical.Service.Framework;
using InsuranceRegistrationTechnical.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace InsuranceRegistrationTechnical.Service.Services;
public class AgeRestrictionEvaluator : IAgeRestrictionEvaluator
{
    private readonly UserRegistrationServiceOptions _options;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AgeRestrictionEvaluator(IOptions<UserRegistrationServiceOptions> options, IDateTimeProvider dateTimeProvider)
    {
        _options = options.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    // This needs work. There are lots of gotchas with dates that I have not taken the time to consider.
    public bool IsOverAgeRestriction(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(_dateTimeProvider.UtcNow);
        var ageRestrictionMinimum = today.AddYears(0 - _options.AgeRestrictionYears);
        return dateOfBirth <= ageRestrictionMinimum;
    }
}
