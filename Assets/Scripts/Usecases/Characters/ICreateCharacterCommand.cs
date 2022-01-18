using Domain.Characters.Types;


namespace Usecases.Characters
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacter Type { get; }
        public EnumDirection Direction { get; }
        public (double, double) Position { get; }
        public float Speed { get; }
    }
}

