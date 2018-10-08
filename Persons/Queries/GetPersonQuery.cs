using Persons.Abstractions;

namespace Persons.Queries
{
    public class GetPersonQuery : IQuery<FindById, IPerson>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonQuery(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        
        public IPerson Ask(FindById input)
        {
            return _personRepository.Find(input.Id);
        }
    }
}