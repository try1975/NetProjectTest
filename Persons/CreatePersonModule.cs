using System;
using System.Globalization;
using Nancy;
using Nancy.ModelBinding;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Logging;

namespace Persons
{
    public class CreatePersonModule: NancyModule
    {
        static string CreatePersonEndpoint = "/api/v1/persons";
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();


        public CreatePersonModule(IPersonFactory personFactory, IPersonRepository personRepository, ICommandHandler<CreatePerson> commandHandler): base(CreatePersonEndpoint)
        {
            Post[""] = parameters =>
            {
                var createPerson = this.Bind<CreatePerson>();
                Logger.InfoFormat("Liblog: {@createPerson}", createPerson);
                commandHandler.Execute(createPerson);

                var personDto = this.Bind<PersonDto>();
                if (!DateTime.TryParseExact(personDto.BirthDay, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var birthDay)) return HttpStatusCode.BadRequest;
                var person = personFactory.CreatePerson(personDto.Name, birthDay);
                if (person == null) return HttpStatusCode.UnprocessableEntity;
                personRepository.Insert(person);
                var response = new Response
                {
                    StatusCode = HttpStatusCode.Created,
                    Headers = {["Location"] = $"{CreatePersonEndpoint}/{person.Id}"}
                };
                return response;
            };
        }
    }
}