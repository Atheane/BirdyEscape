using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Domain.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Frameworks.Dtos;

public class IOGameRepository : IGameRepository
{
    public void Save(IGameEntity gameEntity)
    {
        string destination = Application.persistentDataPath + "/Game.dat";
        FileStream file;
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, new GameDto(
            gameEntity._id,
            gameEntity._currentLevel,
            gameEntity._energy.Value, gameEntity._connectionsDate
           )
         );
        file.Close();
    }

    public IGameEntity Load()
    {
        string destination = Application.persistentDataPath + "/Game.dat";

        FileStream file;
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            throw new Exception("File not found");
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameDto data = (GameDto)bf.Deserialize(file);
        file.Close();
        var charactersEntity = new List<ICharacterEntity>();
        foreach (ICharacterDto characterDto in data._currentLevel._characters)
        {
            charactersEntity.Add(CharacterEntity.Load(
                characterDto._id,
                characterDto._type,
                characterDto._direction,
                VOPosition.Create((characterDto._position.x, characterDto._position.y, characterDto._position.z)),
                characterDto._speed
            ));
        }
        var tilesEntity = new List<ITileEntity>();
        foreach (ITileDto tileDto in data._currentLevel._tiles)
        {
            tilesEntity.Add(TileEntity.Load(
                tileDto._id,
                VOCoordinates.Create(((int)tileDto._position.x, (int)tileDto._position.y)),
                VOPath.Create(tileDto._path)
            ));
        }
        var levelEntity = LevelEntity.Load(data._currentLevel._id, data._currentLevel._number, charactersEntity.ToArray(), tilesEntity.ToArray(), data._currentLevel._state);
        return GameEntity.Load(
            data._id,
            levelEntity,
            VOEnergy.Load(data._energy), data._connectionsDate);
    }
}
