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
    public List<CharacterMoveController> _charactersControllers;
    public List<TileController> _tilesControllers;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public (Guid id, Vector3 position, EnumDirection direction)[] GetCharactersInit()
    {
        List<(Guid id, Vector3 position, EnumDirection direction)> charactersRestartProps = new List<(Guid id, Vector3 position, EnumDirection direction)>();
        foreach (CharacterMoveController controller in _charactersControllers)
        {
            (Guid id, Vector3 position, EnumDirection direction) characterRestartProps = (controller._dto._id, controller._init_position, controller._init_direction);
            charactersRestartProps.Add(characterRestartProps);
        }
        return charactersRestartProps.ToArray();
    }

    public Guid[] GetTilesIdsWithArrows()
    {
        List<Guid> ids = new List<Guid>();
        foreach (TileController controller in _tilesControllers)
        {
            if (controller._dto._arrow != null)
            {
                ids.Add(controller._dto._id);
            }
        }
        return ids.ToArray();
    }

    private void Start()
    {
        SetCharactersController(gameObject);
        SetTilesController(gameObject);
        List<ICharacterEntity> characters = CreateCharacters();
        List<ITileEntity> tiles = CreateTiles();
        ILevelEntity levelEntity = _container.Resolve<CreateLevel>().Execute(
            new CreateLevelCommand(
                1,
                characters,
                tiles,
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

            if (child.TryGetComponent(out CharacterMoveController controller))
            {
                _charactersControllers.Add(controller);
            }
            // reccursive search for child
            SetCharactersController(child.gameObject);
        }
    }

    private List<ICharacterEntity> CreateCharacters()
    {
        List<ICharacterEntity> characters = new List<ICharacterEntity>();
        foreach (CharacterMoveController controller in _charactersControllers)
        {
            ICharacterEntity characterEntity = _container.Resolve<CreateCharacter>().Execute(
                new CreateCharacterCommand(
                    controller._type,
                    controller._init_direction,
                    controller._init_position,
                    (int)controller._speed)
                );
            characters.Add(characterEntity);
            controller.SetDto(CharacterDto.Create(
                    characterEntity._id,
                    characterEntity._type,
                    characterEntity._direction,
                    characterEntity._state,
                    characterEntity._position,
                    characterEntity._speed
            ));
        }
        return characters;
    }

    private void SetTilesController(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;

            if (child.TryGetComponent(out TileController controller))
            {
                _tilesControllers.Add(controller);
            }
            // reccursive search for child
            SetTilesController(child.gameObject);
        }
    }

    private List<ITileEntity> CreateTiles()
    {
        List<ITileEntity> tiles = new List<ITileEntity>();
        foreach (TileController controller in _tilesControllers)
        {
            string path = controller.gameObject.name;
            ITileEntity tileEntity = _container.Resolve<CreateTile>().Execute(new CreateTileCommand(controller.transform.position, path));
            tiles.Add(tileEntity);
            controller.SetDto(TileDto.Create(
                tileEntity._id,
                tileEntity._coordinates,
                tileEntity._path
            ));
        }
        return tiles;
    }
}
