namespace Persons.Abstractions
{
    public interface IQuery<in TInput, out TOutput>
    {
        TOutput Ask(TInput input);
    }
}