using ResultTypeClass.FollowerErrors;
using System.Diagnostics.CodeAnalysis;

namespace ResultTypeClass;

public class Result
{
    protected Result(IReadOnlyList<Error> error)
    {
        Errors = error ?? Array.Empty<Error>();
    }

    public IReadOnlyList<Error> Errors;

    public static Result<List<Error>> Fail(string errorMessage)
    {
        return new Result<List<Error>>(new List<Error>(), true, new List<Error> 
        { ErrorFactory.Failure(errorMessage) });
    }

    public static Result<List<Error>> Fail(Error error)
    {
        return new Result<List<Error>>(new List<Error>(), true, new List<Error>
        { error });
    }

    public static Result<List<Error>> Fail(List<Error> errors)
    {
        return new Result<List<Error>>(new List<Error>(), true, errors);
    }

    public static Result<T> Ok<T>(T value) => new Result<T>(value, false, Array.Empty<Error>());
}

public partial class Result<T> : Result
{
    public T? Value { get; }
    [MemberNotNullWhen(false, nameof(Value))]
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure { get; }
    public Error? Error { get {
            if (!IsFailure)
            {
                return ErrorFactory.Failure("Não há nenhum Error.");
            }

            return Errors[0];
        } 
    }

    protected internal Result(T? value, bool isFail, IReadOnlyList<Error> errors)
        : base(errors)
    {
        Value = value;
        IsFailure = isFail;
    }

    // Operador implícito para criar um Result<T> a partir de um valor
    public static implicit operator Result<T>(T value) => new Result<T>(value, false, Array.Empty<Error>());

    public static implicit operator Result<T>(string description) =>
     new Result<T>(default, true, new List<Error> { ErrorFactory.Failure(description)});


    /// <summary>
    /// Converte implicitamente um resultado contendo uma lista de erros em um resultado de um tipo especificado.
    /// </summary>
    /// <typeparam name="T">O tipo de valor retornado no resultado.</typeparam>
    /// <param name="errorResult">O resultado contendo uma lista de erros.</param>
    /// <remarks>
    /// Este operador permite a conversão implícita de um <see cref="Result{List{Error}}"/> em um <see cref="Result{T}"/>.
    /// No contexto de um método assíncrono como <see cref="Task{Result{Person}} GetResultAsync"/>, se o resultado original for bem-sucedido,
    /// um novo resultado é retornado com o valor especificado. Caso contrário, se o método retornar uma falha representada
    /// por uma lista de erros, um novo resultado é retornado contendo esses erros, conforme exemplificado pela linha
    /// <code> return Result.Fail(failsList); </code> que é implicitamente convertida em um <see cref="Result{Person}"/>.
    /// </remarks>

    public static implicit operator Result<T>(Result<List<Error>> errorResult)
    {
            return new Result<T>(default, true, errorResult.Errors.ToList());
    }

    //public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IReadOnlyList<Error>, TResult> onFailure)
    //{
    //    if (this is Result<T> result)
    //    {
    //        return !result.IsFailure ? onSuccess(result.Value) : onFailure(result.Errors);
    //    }
    //    else
    //    {
    //        throw new InvalidOperationException("Match called on non-generic Result.");
    //    }
    //}

}