using System;
using System.Globalization;
using Nancy;
using Nancy.ModelBinding;
using Persons.Abstractions;

namespace Persons
{
    public class GetPersonModule : NancyModule
    {
        static string GetPersonEndpoint = "/api/v1/persons";

        public GetPersonModule(IPersonRepository personRepository) : base(GetPersonEndpoint)
        {
            Get["{id:Guid}"] = parameters =>
            {
                var id = Guid.Parse(parameters["id"]);
                return personRepository.Find(id);
            };
        }
    }
}