

namespace ResultTypeClass;

public class Person
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    private Person(string name, string lastName)
    {
        Name = name;
        LastName = lastName;
    }

    public static Result<Person> Create(string name, string lastname) => new Person(name, lastname);
}
