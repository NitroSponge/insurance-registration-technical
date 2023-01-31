using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceRegistrationTechnical.Service.Configuration;
public class UserRegistrationServiceOptions
{
    public const string ConfigurationSectionKey = "UserRegistrationServiceOptions";

    public int MinimumNameLength { get; set; } = 3;
    public int MaximumNameLength { get; set; } = 50;
    public string PolicyReferenceRegex { get; set; } = @"[A-Z]{2}-[\d]{6}";
    public int AgeRestrictionYears { get; set; } = 18;
}
