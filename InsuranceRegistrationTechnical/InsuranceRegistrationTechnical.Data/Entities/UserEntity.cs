using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceRegistrationTechnical.Data.Entities;

public class UserEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string PolicyReferenceNumber { get; set; } = default!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Email { get; set; }
}
