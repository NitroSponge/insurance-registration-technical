using Microsoft.AspNetCore.Mvc;

namespace InsuranceRegistrationTechnical.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;

    public RegistrationController(ILogger<RegistrationController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "PostCreateUserRequest")]
    public Task<int> PostCreateUserRequest(CancellationToken cancellationToken)
    {
        return Task.FromResult(1);
    }
}