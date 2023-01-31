using InsuranceRegistrationTechnical.Data.Data;
using InsuranceRegistrationTechnical.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceRegistrationTechnical.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;
    private readonly RegistrationDatabaseContext _databaseContext;

    public RegistrationController(ILogger<RegistrationController> logger, RegistrationDatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpPost(Name = "PostCreateUserRequest")]
    public async Task<int> PostCreateUserRequest(CancellationToken cancellationToken)
    {
        _databaseContext.Add<UserEntity>(new()
        {
            FirstName = "TestFirst",
            Surname = "TestLast",
            PolicyReferenceNumber = "TestRefNum"
        });
        await _databaseContext.SaveChangesAsync(cancellationToken);
        var savedUsers = await _databaseContext.Users.ToListAsync(cancellationToken);
        return savedUsers.Count;
    }
}