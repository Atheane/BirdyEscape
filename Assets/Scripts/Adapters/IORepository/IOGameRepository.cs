using System;
using System.IO;
using UnityEngine;
using Domain.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Frameworks.IO;

namespace Adapters.IORepository
{
    public class IOGameRepository : IGameRepository
    {
        public void Save(IGameEntity gameEntity)
        {
            string filePath = Application.persistentDataPath + "/Game.json";
            var gameIO = new GameIO(
                gameEntity._id,
                gameEntity._currentLevel,
                gameEntity._energy.Value, gameEntity._connectionsDate
               );
            string data = JsonUtility.ToJson(gameIO, true); //pretty print!
            File.WriteAllText(filePath, data);
        }

        public GameIO PreLoad()
        {
            string filePath = Application.persistentDataPath + "/Game.json";

            if (File.Exists(filePath))
            {
                string retrievedData = File.ReadAllText(filePath);
                GameIO gameIO = JsonUtility.FromJson<GameIO>(retrievedData);
                return gameIO;
            }
            throw new Exception("File not found");
        }

        public IGameEntity Load(ILevelEntity levelEntity)
        {
            string filePath = Application.persistentDataPath + "/Game.json";

            if (File.Exists(filePath))
            {
                string retrievedData = File.ReadAllText(filePath);
                GameIO gameIO = JsonUtility.FromJson<GameIO>(retrievedData);
                IGameEntity gameEntity = GameEntity.Load(gameIO._id, levelEntity, VOEnergy.Load(gameIO._energy), gameIO._connectionsDate);
                return gameEntity;
            }
            throw new Exception("File not found");
        }
    }
}
