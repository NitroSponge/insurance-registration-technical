namespace InsuranceRegistrationTechnical.Service.Models;

public class RegisterUserRequestModel
{
    public string FirstName { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string PolicyReferenceNumber { get; set; } = default!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Email { get; set; }
}
