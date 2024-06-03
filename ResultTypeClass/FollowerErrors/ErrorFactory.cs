namespace ResultTypeClass.FollowerErrors;

public class ErrorFactory
{
    public static Error Failure(string description) => Error.Failure("Error.Failure", description);
}
