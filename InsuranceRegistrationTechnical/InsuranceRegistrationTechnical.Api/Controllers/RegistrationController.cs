using InsuranceRegistrationTechnical.Api.Dtos;
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

    [HttpPost(Name = "PostRegisterUserRequest")]
    public async Task<int> PostRegisterUserRequest([FromBody] RegisterUserRequestDto request, CancellationToken cancellationToken)
    {
        await _userRepository.CreateAsync(new()
        {
            FirstName = request.FirstName,
            Surname = request.Surname,
            PolicyReferenceNumber = request.PolicyReferenceNumber
        }, cancellationToken);
        await _userRepository.SaveAsync(cancellationToken);
        var savedUsers = await _userRepository.GetAllAsync(cancellationToken);
        return savedUsers.Count();
    }
}