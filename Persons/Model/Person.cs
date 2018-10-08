using System;
using Persons.Abstractions;

namespace Persons.Model
{
    public class Person : IPerson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }

        public bool IsNameValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public bool IsBirthDayValid(int age)
        {
            return !(age > 120);
        }

        public void ChangeAge(DateTime birthDay)
        {
            var age = GetAge(birthDay);
            if (IsBirthDayValid(age)) Age = age;
        }

        private int GetAge(DateTime birthDay)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age)) age--;

            return age;
        }
    }
}