using Libs.Usecases;
using Domain.Characters.Entities;
using Domain.Characters.ValueObjects;
using Domain.Characters.Types;
using Domain.Characters.Repositories;

namespace Usecases.Characters
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public (float, float) Position { get; }
        public int Speed { get; }
    }
    public class CreateCharacter : IUsecase<ICreateCharacterCommand, ICharacterEntity>
    {
        public ICharactersRepository charactersRepository;
        public CreateCharacter(ICharactersRepository charactersRepository)
        {
            this.charactersRepository = charactersRepository;
        }
        public ICharacterEntity Execute(ICreateCharacterCommand command)
        {
            var position = VOPosition.Create(command.Position);
            var character = CharacterEntity.Create(command.Type, command.Direction, position, command.Speed);

            this.charactersRepository.Add(character);
            return character;
        }
    }
}
