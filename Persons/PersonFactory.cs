using System;
using Persons.Abstractions;

namespace Persons
{
    public class PersonFactory : IPersonFactory
    {
        public IPerson CreatePerson(string name, DateTime bithDay)
        {
            var today = DateTime.Today;
            var age = today.Year - bithDay.Year;
            if (bithDay > today.AddYears(-age)) age--;

            if (age > 120 || string.IsNullOrWhiteSpace(name) || name.Length>100) return null;
            return new Person() { Id = Guid.NewGuid(), Name = name, BirthDay = bithDay, Age = age };
        }
    }
}