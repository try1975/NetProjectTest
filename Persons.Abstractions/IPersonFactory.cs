using System;

namespace Persons.Abstractions
{
    public interface IPersonFactory
    {
        IPerson CreatePerson(string name, DateTime bithDay);
    }
}