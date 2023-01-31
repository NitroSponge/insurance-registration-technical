using InsuranceRegistrationTechnical.Data.Data;
using InsuranceRegistrationTechnical.Data.Entities;
using InsuranceRegistrationTechnical.Data.Interfaces;
using InsuranceRegistrationTechnical.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRegistrationTechnical.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;
    private readonly IUserRepository _userRepository;

    public RegistrationController(ILogger<RegistrationController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost(Name = "PostCreateUserRequest")]
    public async Task<int> PostCreateUserRequest(CancellationToken cancellationToken)
    {
        await _userRepository.CreateAsync(new()
        {
            FirstName = "TestFirst",
            Surname = "TestLast",
            PolicyReferenceNumber = "TestRefNum"
        }, cancellationToken);
        await _userRepository.SaveAsync(cancellationToken);
        var savedUsers = await _userRepository.GetAllAsync(cancellationToken);
        return savedUsers.Count();
    }
}