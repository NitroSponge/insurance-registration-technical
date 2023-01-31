using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceRegistrationTechnical.Service.Interfaces;

namespace InsuranceRegistrationTechnical.Service.Framework;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
