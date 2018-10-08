using System.Runtime.InteropServices.ComTypes;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Logging;
using CreatePerson = Persons.Commands.CreatePerson;

namespace Persons
{
    public class PersonBootstrapper : DefaultNancyBootstrapper
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IPersonFactory, PersonFactory>();
            container.Register<IPersonRepository, SqLiteAndDapperPersonRepository>().AsSingleton();

            container.Register<ICommandHandler<CreatePerson>, CreatePersonHandler>();

            pipelines.BeforeRequest += ctx =>
            {
                var r = ctx.Request;
                Logger.Info($"{r.Method} {r.Path}");
                return null;
            };

            pipelines.OnError += (ctx, exception) =>
            {
                var r = ctx.Request;
                Logger.Error($"{r.Method} {r.Path} {exception}");
                return null;
            };
        }
    }

}