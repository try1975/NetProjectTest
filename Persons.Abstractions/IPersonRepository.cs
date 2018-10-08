using System;

namespace Persons.Abstractions
{
    public interface IPersonRepository
    {
        IPerson Find(Guid id);
        void Insert(IPerson item);
    }
}