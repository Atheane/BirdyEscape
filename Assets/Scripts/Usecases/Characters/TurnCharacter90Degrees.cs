using Libs.Usecases;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Characters.Types;
using Domain.Characters.Entities;
using UnityEngine;


namespace Usecases.Characters
{
    public class TurnCharacter90Degrees : IUsecase<ITurnCharacter90DegreesCommand, EnumCharacterDirection>
    {
        public ICharactersRepository _charactersRepository;
        public ICharacterEntity _characterEntity;
        public TurnCharacter90Degrees(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }
        public EnumCharacterDirection Execute(ITurnCharacter90DegreesCommand command)
        {
            _characterEntity = _charactersRepository.Find(command._characterId);
            Debug.Log("before, entity direction");
            Debug.Log(_characterEntity.Direction);
            _characterEntity.Turn90Degrees();
            Debug.Log("AFTER, entity direction");
            Debug.Log(_characterEntity.Direction);
            return _characterEntity.Direction;
        }
    }
}


