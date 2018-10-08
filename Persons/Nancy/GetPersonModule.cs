using System;
using AutoMapper;
using Nancy;
using Persons.Abstractions;
using Persons.Dto;
using Persons.Queries;

namespace Persons.Nancy
{
    public class GetPersonModule : NancyModule
    {
        static string GetPersonEndpoint = "/api/v1/persons";

        public GetPersonModule(IQuery<FindById, IPerson> query) : base(GetPersonEndpoint)
        {
            Get["{id:Guid}"] = parameters =>
            {
                var id = Guid.Parse(parameters["id"]);
                var person = query.Ask(new FindById {Id = id});
                if (person == null) return HttpStatusCode.NotFound;
                return Mapper.Map<PersonDto>(person);
            };
        }
    }
}