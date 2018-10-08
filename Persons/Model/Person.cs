using System;
using Persons.Abstractions;

namespace Persons.Model
{
    public class Person : IPerson
    {
        private DateTime _birthDay;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay
        {
            get => _birthDay;
            set
            {
                _birthDay = value; 
                ChangeAge(_birthDay);
            }
        }

        public int Age { get; set; }

        public bool IsFieldsValid()
        {
            return IsNameValid() && IsBirthDayValid(Age);
        }

        private bool IsNameValid()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        private bool IsBirthDayValid(int age)
        {
            return !(age > 120);
        }

        private void ChangeAge(DateTime birthDay)
        {
            var age = GetAge(birthDay);
            if (IsBirthDayValid(age)) Age = age;
        }

        private static int GetAge(DateTime birthDay)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age)) age--;

            return age;
        }
    }
}