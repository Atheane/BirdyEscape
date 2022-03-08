using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Domain.Entities;
using Usecases;
using Usecases.Commands;
using Domain.Types;
using Frameworks.Dtos;

public class LevelController : MonoBehaviour
{

    public ILevelDto _dto;
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
        List<ICharacterEntity> characters = CreateCharactersAtInitPosition();
        ILevelEntity levelEntity = _container.Resolve<CreateLevel>().Execute(
            new CreateLevelCommand(
                1,
                characters,
                EnumLevelState.OFF
            ));
        _dto = LevelDto.Create(levelEntity._id, levelEntity._number, levelEntity._characters, levelEntity._state);
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

    private List<ICharacterEntity> CreateCharactersAtInitPosition()
    {
        List<ICharacterEntity> characters = new List<ICharacterEntity>();
        foreach (CharacterMoveController controller in _charactersController)
        {
            ICharacterEntity characterEntity = _container.Resolve<CreateCharacter>().Execute(
                new CreateCharacterCommand(
                    controller._type,
                    controller._init_direction,
                    controller._init_position,
                    controller._speed)
                );
            characters.Add(characterEntity);
        }
        return characters;
    }

    public (Guid id, Vector3 position, EnumDirection direction)[] GetCharactersInit()
    {
        List<(Guid id, Vector3 position, EnumDirection direction)> charactersRestartProps = new List<(Guid id, Vector3 position, EnumDirection direction)>();
        foreach (CharacterMoveController controller in _charactersController)
        {
            (Guid id, Vector3 position, EnumDirection direction) characterRestartProps = (controller._dto._id, controller._init_position, controller._init_direction);
            charactersRestartProps.Add(characterRestartProps);
        }
        return charactersRestartProps.ToArray();
    }
}
