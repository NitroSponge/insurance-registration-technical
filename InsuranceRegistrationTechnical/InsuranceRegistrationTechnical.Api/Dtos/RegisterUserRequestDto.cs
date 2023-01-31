namespace InsuranceRegistrationTechnical.Api.Dtos;

public class RegisterUserRequestDto
{
    public string FirstName { get; init; } = default!;

    public string Surname { get; init; } = default!;

    public string PolicyReferenceNumber { get; init; } = default!;

    public DateOnly? DateOfBirth { get; init; }

    public string? Email { get; init; }
}
