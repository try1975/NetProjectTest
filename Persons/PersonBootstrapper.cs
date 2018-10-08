using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Persons.Abstractions;
using Persons.Commands;
using CreatePerson = Persons.Commands.CreatePerson;

namespace Persons
{
    public class PersonBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IPersonFactory, PersonFactory>();
            container.Register<IPersonRepository, PersonRepository>();

            container.Register<ICommandHandler<CreatePerson>, CreatePersonHandler>();
        }
    }
}