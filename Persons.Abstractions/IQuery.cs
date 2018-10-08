namespace Persons.Abstractions
{
    public interface IQuery<out TOutput>
    {
        TOutput Ask();
    }
}