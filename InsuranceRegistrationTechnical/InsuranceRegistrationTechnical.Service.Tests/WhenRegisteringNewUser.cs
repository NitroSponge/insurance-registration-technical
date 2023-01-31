using AutoFixture;
using FluentAssertions;
using InsuranceRegistrationTechnical.Data.Interfaces;
using InsuranceRegistrationTechnical.Service.Configuration;
using InsuranceRegistrationTechnical.Service.Interfaces;
using InsuranceRegistrationTechnical.Service.Models;
using InsuranceRegistrationTechnical.Service.Services;
using InsuranceRegistrationTechnical.Service.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace InsuranceRegistrationTechnical.Service.Tests;

public class WhenRegisteringNewUser
{
    private UserRegistrationService Service { get; set; }
    private Mock<ILogger<UserRegistrationService>> MockLogger { get; set; }
    private Mock<IUserRepository> MockUserRepository { get; set; }
    private Mock<IAgeRestrictionEvaluator> MockAgeRestrictionEvaluator { get; set; }
    private RegisterUserRequestModel Request { get; set; }
    private ServiceResult<int> Result { get; set; }

    // In my previous workplaces, frameworks have existed for auto-handling DI in tests. I am unsure how to do this myself without further reading.
    [SetUp]
    public void Setup()
    {
        MockLogger = new Mock<ILogger<UserRegistrationService>>();
        MockUserRepository = new Mock<IUserRepository>();
        MockAgeRestrictionEvaluator = new Mock<IAgeRestrictionEvaluator>();
        var options = Options.Create(new UserRegistrationServiceOptions());
        Service = new UserRegistrationService(MockLogger.Object, MockUserRepository.Object, MockAgeRestrictionEvaluator.Object, options);
        Request = null!;
        Result = null!;
    }

    [Test]
    [TestCase("")]
    [TestCase("x@example.com")]
    [TestCase("xx@example.com")]
    [TestCase("invalidDomain@example.co")]
    [TestCase("invalidDomain@example.xx")]
    [TestCase("invalidDomain@x.com")]
    public async Task When_registering_with_invalid_email(string invalidEmail)
    {
        Given_user_request_model_with_invalid_email(invalidEmail);
        await When_registering_new_user();
        Then_should_return_failed_result();
    }

    private void Given_user_request_model_with_invalid_email(string invalidEmail)
    {
        Given_valid_user_request_model();
        Request.Email = invalidEmail;
    }

    private async Task When_registering_new_user()
    {
        Result = await Service.RegisterUserAsync(Request, default);
    }

    private void Then_should_not_attempt_to_create_user()
    {
        MockUserRepository.Verify(repo => repo.CreateAsync(default, default), Times.Never);
    }

    private void Then_should_return_failed_result()
    {
        Result.IsFailure.Should().BeTrue();
    }

    private void Given_valid_user_request_model()
    {
        Request = new RegisterUserRequestModel()
        {
            FirstName = "TestFirstName",
            Surname = "TestSurname",
            PolicyReferenceNumber = "XX-123456",
            DateOfBirth = new DateOnly(1992, 01, 29),
            Email = "example@test.com"
        };
    }
}