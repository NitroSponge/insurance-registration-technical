namespace InsuranceRegistrationTechnical.Service.Interfaces;

public interface IAgeRestrictionEvaluator
{
    bool IsOverAgeRestriction(DateOnly dateOfBirth);
}