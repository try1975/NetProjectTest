using System;

namespace Persons.Commands
{
    public class CreatePerson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BirthDay { get; set; }
    }
}