using Libs.Usecases;
using Usecases.Characters.Queries;
using Domain.Characters.Entities;
using Domain.Characters.ValueObjects;
using Domain.Characters.Repositories;


namespace Usecases.Characters
{
    public class GetCharacterPosition : IUsecase<IGetCharacterPositionQuery, VOPosition>
    {
        public ICharactersRepository _charactersRepository;
        public GetCharacterPosition(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }
        public VOPosition Execute(IGetCharacterPositionQuery query)
        {
            var character = _charactersRepository.Find(query._characterId);
            return character.Position;
        }
    }
}