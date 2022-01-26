using Libs.Usecases;
using Usecases.Characters.Commands;
using Domain.Characters.Entities;
using Domain.Characters.ValueObjects;
using Domain.Characters.Repositories;

namespace Usecases.Characters
{
    public class CreateCharacter : IUsecase<ICreateCharacterCommand, ICharacterEntity>
    {
        public ICharactersRepository _charactersRepository;
        public CreateCharacter(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }
        public ICharacterEntity Execute(ICreateCharacterCommand command)
        {
            var position = VOPosition.Create(command.Position);
            var character = CharacterEntity.Create(command.Type, command.Direction, position, command.Speed);

            _charactersRepository.Add(character);
            return character;
        }
    }
}
