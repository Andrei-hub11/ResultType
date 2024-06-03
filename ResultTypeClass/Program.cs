using ResultTypeClass;
using ResultTypeClass.FollowerErrors;

Console.WriteLine("Hello, World!");

var result = Result.Fail("haha");

Console.WriteLine("Resultado: " + result.Error.Description);

var newPerson = Person.Create("Andrey", "Rodriguez");

Console.WriteLine($"Pessoa: {newPerson.Value.Name} {newPerson.Value.LastName}");

var fail = Result.Fail("Resultado de falha");

Console.WriteLine("Resultado: " + fail.Error.Description);


var value = Getvalue();

var value2 = value.Match(
    onSuccess: value => $"{value} errors occurred.",
    onFailure: errors => $"{errors.Count} errors occurred."
);

Console.WriteLine("Resultadoo: " + value.Value);

static Result<bool> Getvalue()
{
    return Result.Ok(true);
}

var result3 = await GetResultAsync(1);

if (!result3.IsFailure)
{
    Console.WriteLine(result3.Value.Name);
    Console.WriteLine(result3.Error.Description);
}

var result4 = await GetResultAsync(2);

var value3 = result3.Match(
   onSuccess: value => $"{value.Name}",
   onFailure: errors => $"{string.Join(", ", errors.Select(error => error.Description).ToList())}"
   );

var value4 = result4.Match(
   onSuccess: value => $"{value.Name} {value.LastName}",
   onFailure: errors => $"{string.Join(", ", fail.Errors.Select(error => error.Description).ToList())}"
   );

Console.WriteLine(value3);
Console.WriteLine(value4);

async static Task<Result<Person>> GetResultAsync(int id)
{
    await Task.Delay(1);

    var result = Person.Create("Andrade", "Wick");

    var failure = ErrorFactory.Failure("Error ao tentar gerar a pessoa.");

    return !result.IsFailure ? Result.Ok(result.Value): Result.Fail(failure);
}