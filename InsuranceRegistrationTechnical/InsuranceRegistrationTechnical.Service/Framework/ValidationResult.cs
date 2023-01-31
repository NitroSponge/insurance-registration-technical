using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceRegistrationTechnical.Service.Framework;
public class ValidationResult
{
    public bool IsValid => !_errors.Any();

    public IEnumerable<string> Errors => _errors;

    private List<string> _errors;

    public ValidationResult()
    {
        _errors = new List<string>();
    }

    public void AddError(string errorMessage)
    {
        _errors.Add(errorMessage);
    }
}
