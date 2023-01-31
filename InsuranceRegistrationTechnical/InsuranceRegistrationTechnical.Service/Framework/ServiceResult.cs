namespace InsuranceRegistrationTechnical.Service.Tools;

public class ServiceResult
{
    public static ServiceResult<TValue> SuccessFromValue<TValue>(TValue value)
        => ServiceResult<TValue>.SuccessFromValue(value);

    public static ServiceResult<TValue> FailureFromErrors<TValue>(IEnumerable<string> errors)
        => ServiceResult<TValue>.FailureFromErrors(errors);
}

public class ServiceResult<TValue> : ServiceResult
{
    public TValue? Value { get; }
    public IEnumerable<string>? Errors => _errors;
    public bool IsSuccess => Value != null && _errors == null;
    public bool IsFailure => !IsSuccess;

    private readonly IEnumerable<string>? _errors;

    private ServiceResult(TValue? value, IEnumerable<string>? errors)
    {
        if (value == null && (errors == null || !errors.Any()))
        {
            throw new InvalidOperationException("ServiceResult must have either a value or error messages.");
        }
        Value = value;
        _errors = errors;
    }

    public static ServiceResult<TValue> SuccessFromValue(TValue value)
        => new (value, null);

    public static ServiceResult<TValue> FailureFromErrors(IEnumerable<string> errors)
        => new (default, errors);
}
