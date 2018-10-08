namespace Persons.Abstractions
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}