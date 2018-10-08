using System.Threading.Tasks;

namespace Persons.Commands
{
    public interface ICommandHandler<in TCommand>
    {
        bool Execute(TCommand command);
    }
}