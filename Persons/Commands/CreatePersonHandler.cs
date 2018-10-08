using System;
using System.Globalization;
using Persons.Abstractions;

namespace Persons.Commands
{
    public class CreatePersonHandler : ICommandHandler<CreatePerson>
    {
        private readonly IPersonFactory _personFactory;
        private readonly IPersonRepository _personRepository;

        public CreatePersonHandler(IPersonFactory personFactory, IPersonRepository personRepository)
        {
            _personFactory = personFactory;
            _personRepository = personRepository;
        }

        public void Handle(CreatePerson command)
        {
            
            if (!DateTime.TryParseExact(command.BirthDay, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var birthDay)) return;
            var person = _personFactory.CreatePerson(command.Name, birthDay);
            _personRepository.Insert(person);
        }
    }
}