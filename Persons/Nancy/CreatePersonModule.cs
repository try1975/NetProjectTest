using System;
using System.Globalization;
using Nancy;
using Nancy.ModelBinding;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Logging;

namespace Persons.Nancy
{
    public class CreatePersonModule: NancyModule
    {
        private const string CreatePersonEndpoint = "/api/v1/persons";
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();


        public CreatePersonModule(IPersonRepository personRepository, ICommandHandler<CreatePerson> commandHandler): base(CreatePersonEndpoint)
        {
            Post[""] = parameters =>
            {
                var createPerson = this.Bind<CreatePerson>();
                Logger.InfoFormat("{@createPerson}", createPerson);
                if (!DateTime.TryParseExact(createPerson.BirthDay, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out _)) return HttpStatusCode.BadRequest;
                commandHandler.Handle(createPerson);
                if (createPerson.Id == Guid.Empty) return HttpStatusCode.UnprocessableEntity;
                var person = personRepository.Find(createPerson.Id);
                if (person == null) return HttpStatusCode.UnprocessableEntity;
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