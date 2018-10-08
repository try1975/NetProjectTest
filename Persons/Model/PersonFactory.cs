using System;
using Persons.Abstractions;

namespace Persons.Model
{
    public class PersonFactory : IPersonFactory
    {
        public IPerson CreatePerson(string name, DateTime birthDay)
        {
            var person = new Person() { Id = Guid.NewGuid(), Name = name, BirthDay = birthDay};
            return !person.IsFieldsValid() ? null : person;
        }
    }
}