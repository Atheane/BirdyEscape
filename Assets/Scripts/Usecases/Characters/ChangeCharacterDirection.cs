using Libs.Usecases;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Characters.Types;
using Domain.Characters.Entities;
using UnityEngine;


namespace Usecases.Characters
{
    public class ChangeCharacterDirection : IUsecase<IChangeCharacterDirectionCommand, EnumCharacterDirection>
    {
        public ICharactersRepository _charactersRepository;
        public ICharacterEntity _characterEntity;
        public ChangeCharacterDirection(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }
        public EnumCharacterDirection Execute(IChangeCharacterDirectionCommand command)
        {
            _characterEntity = _charactersRepository.Find(command._characterId);
            Debug.Log("before, entity direction");
            Debug.Log(_characterEntity.Direction);
            _characterEntity.UpdateDirection(command._direction);
            Debug.Log("AFTER, entity direction");
            Debug.Log(_characterEntity.Direction);
            return _characterEntity.Direction;
        }
    }
}


