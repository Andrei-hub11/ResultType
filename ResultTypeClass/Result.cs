public class Error
{
    public string Description { get; }

    public Error(string description)
    {
        Description = description;
    }
}

public class Result
{
    private readonly List<Error> _errors;

    protected Result(bool success, List<Error> error)
    {
        Success = success;
        _errors = error;
    }

    public bool Success { get; }
    public IReadOnlyList<Error> Errors => _errors;
    public bool IsFailure => !Success;

    public static Result<List<Error>> Fail(string errorMessage)
    {
        return new Result<List<Error>>(new List<Error>(), false, new List<Error> { new Error(errorMessage) });
    }

    public static Result<List<Error>> Fail(List<Error> errors)
    {
        return new Result<List<Error>>(new List<Error>(), false, errors);
    }

    public static Result<T> Ok<T>(T value) => new Result<T>(value, true, new List<Error>());
}

public class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool success, List<Error> error)
        : base(success, error)
    {
        Value = value;
    }

    // Operador implícito para criar um Result<T> a partir de um valor
    public static implicit operator Result<T>(T value) => new Result<T>(value, true, new List<Error>());

    public static implicit operator Result<T>(string description) =>
     new Result<T>(default, false, new List<Error> { new Error (description)});

    public static implicit operator Result<T>(Result<List<Error>> errorResult)
    {
        if (errorResult == null)
            throw new ArgumentNullException(nameof(errorResult));

        if (typeof(T) is null)
            throw new ArgumentNullException(nameof(T));

        if (!errorResult.Success)
            return new Result<T>(default, false, errorResult.Errors.ToList());

        return Ok(default(T) ?? throw new InvalidOperationException("Default value for reference type is null"));
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IReadOnlyList<Error>, TResult> onFailure)
    {
        if (this is Result<T> result)
        {
            return result.Success ? onSuccess(result.Value) : onFailure(result.Errors);
        }
        else
        {
            throw new InvalidOperationException("Match called on non-generic Result.");
        }
    }

}