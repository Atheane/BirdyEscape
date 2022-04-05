using System;
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
        string destination = Application.dataPath + "/Persistence/Game.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, new GameDto(gameEntity._id, gameEntity._currentLevelNumber, gameEntity._energy.Value, gameEntity._firstConnectionDate));
        file.Close();
    }

    public IGameEntity Load()
    {
        string destination = Application.dataPath + "/Persistence/Game.dat";
        FileStream file;
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            throw new System.Exception("File not found");
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameDto data = (GameDto)bf.Deserialize(file);
        file.Close();
        return GameEntity.Load(data._id, data._currentLevel, VOEnergy.Create(data._energy), data._firstConnectionDate);
    }
}
