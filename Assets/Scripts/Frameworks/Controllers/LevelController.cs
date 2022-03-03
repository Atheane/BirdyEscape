using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Domain.Entities;
using Usecases;
using Usecases.Commands;
using Domain.Types;

public class LevelController : MonoBehaviour
{

    //public LevelDto _dto;
    private DiContainer _container;
    public List<CharacterMoveController> _charactersController;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        SetCharactersController(gameObject);
        List<ICharacterEntity> characters = new List<ICharacterEntity>();

        foreach (CharacterMoveController controller in _charactersController)
        {
            ICharacterEntity characterEntity = _container.Resolve<CreateCharacter>().Execute(
                new CreateCharacterCommand(
                    controller._type,
                    controller._direction,
                    controller._init_position,
                    controller._speed)
                );
            characters.Add(characterEntity);
        }

        ILevelEntity levelEntity = _container.Resolve<CreateLevel>().Execute(
            new CreateLevelCommand(
                1,
                characters,
                EnumLevelState.OFF
            ));
        // to-do DTO
    }

    private void SetCharactersController(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;

            if (child.TryGetComponent(out CharacterMoveController characterController))
            {

                _charactersController.Add(characterController);
            }
            // reccursive search for child
            SetCharactersController(child.gameObject);
        }
    }
}
