using Libs.Usecases;
using Domain.Characters.Entities;
using Domain.Characters.ValueObjects;

namespace Usecases.Characters
{
    public class CreateCharacter : IUsecase<ICreateCharacterCommand, ICharacterEntity>
    {
        //public ICharactersRepository charactersRepository;
        //public CreateCharacter(ICharactersRepository charactersRepository)
        //{
        //    this.charactersRepository = charactersRepository;
        //}
        public ICharacterEntity Execute(ICreateCharacterCommand command)
        {
            var position = VOPosition.Create(command.Position);
            var character = CharacterEntity.Create(command.Type, command.Direction, position, command.Speed);

            //this.charactersRepository.Add(character);
            character.MoveAlways();
            return character;
        }
    }
}
