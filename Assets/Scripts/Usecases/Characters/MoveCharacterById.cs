using System;
using Libs.Usecases;
using Domain.Characters.Repositories;
using Domain.Characters.Types;


namespace Usecases.Characters
{
    public interface IMoveCharacterByIdCommand
    {
        Guid CharacterId { get; }
    }

    public class MoveCharacterById : IUsecase<IMoveCharacterByIdCommand, EnumCharacterState>
    {
        public ICharactersRepository charactersRepository;
        public MoveCharacterById(ICharactersRepository charactersRepository)
        {
            this.charactersRepository = charactersRepository;
        }
        public EnumCharacterState Execute(IMoveCharacterByIdCommand command)
        {
            var character = this.charactersRepository.Find(command.CharacterId);
            character.MoveAlways();
            return character.State;
        }
    }
}

