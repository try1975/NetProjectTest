using System;

namespace Persons.Abstractions
{
    public interface IPersonRepository
    {
        IPerson Find(Guid id);
        void Insert(IPerson item);
    }

    //public interface ICommandHandler<in TInput>
    //{
    //    void Handle(TInput input);
    //}

    //public interface IQuery<out TOutput>
    //{
    //    TOutput Ask();
    //}

    

    public interface IPerson
    {
        Guid Id { get; set; }
        string Name { get; set; }
        DateTime BirthDay { get; set; }
        int Age { get; set; }
    }

    public class PersonDto 
    {
        public string Name { get; set; }
        public string BirthDay { get; set; }
        public int Age { get; set; }
    }

    public class Person : IPerson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
    }


}