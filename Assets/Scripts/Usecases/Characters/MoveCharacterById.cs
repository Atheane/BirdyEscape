using Libs.Usecases;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Characters.ValueObjects;


namespace Usecases.Characters
{
    public class MoveAlwaysCharacterById : IUsecase<IMoveAlwaysCharacterByIdCommand, VOPosition>
    {
        public ICharactersRepository _charactersRepository;
        public MoveAlwaysCharacterById(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }
        public VOPosition Execute(IMoveAlwaysCharacterByIdCommand command)
        {
            var character = _charactersRepository.Find(command.CharacterId);
            character.MoveAlways();
            return character.Position;
        }
    }
}

