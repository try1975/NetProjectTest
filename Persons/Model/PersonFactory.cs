using System;
using Persons.Abstractions;

namespace Persons.Model
{
    public class PersonFactory : IPersonFactory
    {
        public IPerson CreatePerson(string name, DateTime birthDay)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age)) age--;

            if (age > 120 || string.IsNullOrWhiteSpace(name) || name.Length>100) return null;
            return new Person() { Id = Guid.NewGuid(), Name = name, BirthDay = birthDay, Age = age };
        }
    }
}