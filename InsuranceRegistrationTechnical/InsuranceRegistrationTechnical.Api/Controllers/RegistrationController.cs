using AutoMapper;
using InsuranceRegistrationTechnical.Api.Dtos;
using InsuranceRegistrationTechnical.Data.Data;
using InsuranceRegistrationTechnical.Data.Entities;
using InsuranceRegistrationTechnical.Data.Interfaces;
using InsuranceRegistrationTechnical.Data.Repositories;
using InsuranceRegistrationTechnical.Service.Interfaces;
using InsuranceRegistrationTechnical.Service.Models;
using InsuranceRegistrationTechnical.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRegistrationTechnical.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;
    private readonly IUserRegistrationService _userRegistrationService;
    private readonly IMapper _mapper;

    public RegistrationController(ILogger<RegistrationController> logger, IUserRegistrationService userRegistrationService, IMapper mapper)
    {
        _logger = logger;
        _userRegistrationService = userRegistrationService;
        _mapper = mapper;
    }

    [HttpPost(Name = "PostRegisterUserRequest")]
    public async Task<int> PostRegisterUserRequest([FromBody] RegisterUserRequestDto request, CancellationToken cancellationToken)
    {
        return await _userRegistrationService.RegisterUserAsync(_mapper.Map<RegisterUserRequestModel>(request), cancellationToken);
    }
}