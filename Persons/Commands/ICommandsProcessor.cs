using System.Threading.Tasks;


namespace Persons.Commands
    {
        public interface ICommandsProcessor
        {
            Task Process<TCommand>(TCommand command);
        }
    }

