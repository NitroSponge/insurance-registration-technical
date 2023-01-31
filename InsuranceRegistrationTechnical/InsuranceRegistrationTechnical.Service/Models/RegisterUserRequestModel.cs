namespace InsuranceRegistrationTechnical.Service.Models;

public class RegisterUserRequestModel
{
    public string FirstName { get; init; } = default!;

    public string Surname { get; init; } = default!;

    public string PolicyReferenceNumber { get; init; } = default!;

    public DateOnly? DateOfBirth { get; init; }

    public string? Email { get; init; }
}
