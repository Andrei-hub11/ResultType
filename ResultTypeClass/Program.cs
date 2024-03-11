using ResultTypeClass;

Console.WriteLine("Hello, World!");

var result = Result.Fail("haha");

Console.WriteLine("Resultado: " + result.Errors);

var newPerson = Person.Create("Andrey", "Rodriguez");

Console.WriteLine($"Pessoa: {newPerson.Value.Name} {newPerson.Value.LastName}");

var fail = Result.Fail("Resultado de falha");

Console.WriteLine("Resultado: " + string.Join(", ", fail.Errors.Select(error => error.Description).ToList()));

var listFails = new List<Error>
{
    new Error("Teste 1"),
    new Error("Teste 2"),
    new Error("Teste 3"),
    new Error("Teste 4"),
    new Error("Teste 4")
};

var fails = Result.Fail(listFails);


var foo = fails.Match(
    onSuccess: value => value,
    onFailure: errors => errors
);

var value = Getvalue();

var value2 = value.Match(
    onSuccess: value => $"{value} errors occurred.",
    onFailure: errors => $"{errors.Count} errors occurred."
);

Console.WriteLine("Resultadoo: " + value.Value);
Console.WriteLine(foo);
Console.WriteLine("Resultado de falhas: " + string.Join(", ", fails.
    Errors.Select(error => error.Description).ToList()));

static Result<bool> Getvalue()
{
    return Result.Ok(true);
}

var result3 = await GetResultAsync(1);
var result4 = await GetResultAsync(2);

var value3 = result3.Match(
   onSuccess: value => $"{value}",
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
    if (id == 1)
    {
        var listFails = new List<Error>
{
    new Error("Teste 1"),
    new Error("Teste 2"),
    new Error("Teste 3"),
};
        return Result.Fail(listFails);
    }

    return Result.Ok(Person.Create("Andrade", "Wick").Value);
}