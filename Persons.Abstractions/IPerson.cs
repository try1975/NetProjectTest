using System;

namespace Persons.Abstractions
{
    public interface IPerson
    {
        Guid Id { get; set; }
        string Name { get; set; }
        DateTime BirthDay { get; set; }
        int Age { get; set; }
    }
}