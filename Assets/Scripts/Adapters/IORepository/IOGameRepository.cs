using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Domain.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Frameworks.Dtos;

public class IOGameRepository : IGameRepository
{
    public void Save(IGameEntity gameEntity)
    {
        string filePath = Application.persistentDataPath + "/Game.json";
        var gameDto = new GameDto(
            gameEntity._id,
            gameEntity._currentLevel,
            gameEntity._energy.Value, gameEntity._connectionsDate
           );
        string data = JsonUtility.ToJson(gameDto, true); //pretty print!
        File.WriteAllText(filePath, data);
    }

    public IGameEntity Load()
    {
        string filePath = Application.persistentDataPath + "/Game.json";
        GameDto gameDto;

        if (File.Exists(filePath))
        {
            string retrievedData = File.ReadAllText(filePath);
            gameDto = JsonUtility.FromJson<GameDto>(retrievedData);
        }
        else
        {
            throw new Exception("File not found");
        }
       
        var charactersEntity = new List<ICharacterEntity>();
        foreach (CharacterDto characterDto in gameDto._currentLevel._characters)
        {
            charactersEntity.Add(CharacterEntity.Load(
                Guid.Parse(characterDto._id),
                characterDto._type,
                characterDto._direction,
                VOPosition.Create((characterDto._position.x, characterDto._position.y, characterDto._position.z)),
                characterDto._speed
            ));
        }
        var tilesEntity = new List<ITileEntity>();
        //foreach (TileDto tileDto in gameDto._currentLevel._tiles)
        //{
        //    tilesEntity.Add(TileEntity.Load(
        //        tileDto._id,
        //        VOCoordinates.Create(((int)tileDto._position.x, (int)tileDto._position.y)),
        //        VOPath.Create(tileDto._path)
        //    ));
        //}
        var levelEntity = LevelEntity.Load(gameDto._currentLevel._id, gameDto._currentLevel._number, charactersEntity.ToArray(), tilesEntity.ToArray(), gameDto._currentLevel._state);
        return GameEntity.Load(
            gameDto._id,
            levelEntity,
            VOEnergy.Load(gameDto._energy), gameDto._connectionsDate);
    }
}
