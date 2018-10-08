using AutoMapper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Db;
using Persons.Dto;
using Persons.Logging;
using Persons.Model;
using Persons.Queries;
using CreatePerson = Persons.Commands.CreatePerson;

namespace Persons.Nancy
{
    public class PersonBootstrapper : DefaultNancyBootstrapper
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IPersonFactory, PersonFactory>();
            container.Register<IPersonRepository, SqLiteAndDapperPersonRepository>().AsSingleton();

            container.Register<ICommandHandler<CreatePerson>, CreatePersonHandler>();
            container.Register<IQuery<FindById, IPerson>, GetPersonQuery>();

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

           Mapper.Initialize(cfg=> cfg.CreateMap<IPerson, PersonDto>());
        }
    }

}