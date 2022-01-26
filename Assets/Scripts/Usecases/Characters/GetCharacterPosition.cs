using System;
using Libs.Usecases;
using Domain.Characters.ValueObjects;
using Domain.Characters.Repositories;


namespace Usecases.Characters
{
    public class GetCharacterPositionById : IUsecase<Guid, VOPosition>
    {
        public ICharactersRepository _charactersRepository;
        public GetCharacterPositionById(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }
        public VOPosition Execute(Guid id)
        {
            var character = _charactersRepository.Find(id);
            return character.Position;
        }
    }
}

