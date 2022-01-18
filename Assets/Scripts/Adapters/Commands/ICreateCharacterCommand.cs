using Domain.Characters.Types;


namespace Adapters.Commands
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacter Type { get; }
        public EnumDirection Direction { get; }
        public (double, double) Position { get; }
        public float Speed { get; }
    }
}

