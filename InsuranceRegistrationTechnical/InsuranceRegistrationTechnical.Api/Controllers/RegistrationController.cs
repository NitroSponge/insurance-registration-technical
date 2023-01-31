using AutoMapper;
using InsuranceRegistrationTechnical.Api.Dtos;
using InsuranceRegistrationTechnical.Service.Interfaces;
using InsuranceRegistrationTechnical.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    public async Task<ActionResult<int>> PostRegisterUserRequest([FromBody] RegisterUserRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            // Please note I have assumed from the technical description provided the response requires returning ONLY a customer ID as an integer.
            // Perhaps a more usual response would be a Json object with a CustomerId field and optional array of errors, in real life I would clarify the requirement.
            var result = await _userRegistrationService.RegisterUserAsync(_mapper.Map<RegisterUserRequestModel>(request), cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}