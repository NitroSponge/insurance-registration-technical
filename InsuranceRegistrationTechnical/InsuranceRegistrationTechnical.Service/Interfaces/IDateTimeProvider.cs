namespace InsuranceRegistrationTechnical.Service.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}