using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface ICreateArrowCommand
    {
        public EnumDirection Direction { get; }
        public (int, int) Position { get; }
    }
    public class CreateArrowCommand : IMulticastMessage, ICreateArrowCommand
    {
        public EnumDirection Direction { get; }
        public (int, int) Position { get; }

        public CreateArrowCommand(EnumDirection direction, (int, int) position)
        {
            Direction = direction;
            Position = position;
        }
    }
}
