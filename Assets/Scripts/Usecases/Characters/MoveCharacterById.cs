using System;
using Libs.Usecases;
using Domain.Characters.Repositories;
using Domain.Characters.ValueObjects;


namespace Usecases.Characters
{
    public interface IMoveAlwaysCharacterByIdCommand
    {
        Guid CharacterId { get; }
    }

    public class MoveAlwaysCharacterById : IUsecase<IMoveAlwaysCharacterByIdCommand, VOPosition>
    {
        public ICharactersRepository charactersRepository;
        public MoveAlwaysCharacterById(ICharactersRepository charactersRepository)
        {
            this.charactersRepository = charactersRepository;
        }
        public VOPosition Execute(IMoveAlwaysCharacterByIdCommand command)
        {
            var character = this.charactersRepository.Find(command.CharacterId);
            character.MoveAlways();
            return character.Position;
        }
    }
}

