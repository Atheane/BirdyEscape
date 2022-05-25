using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Domain.Repositories;
using Domain.Entities;
using Domain.ValueObjects;
using Frameworks.IO;
using Adapters.Exceptions;


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
            throw new AppException.FileNotFound("at path" + filePath);
        }

        public IGameEntity Load(ILevelEntity levelEntity)
        {
            string filePath = Application.persistentDataPath + "/Game.json";

            if (File.Exists(filePath))
            {
                string retrievedData = File.ReadAllText(filePath);
                GameIO gameIO = JsonUtility.FromJson<GameIO>(retrievedData);
                var connectionsDate = new List<DateTime>();
                foreach (JsonDateTime date in gameIO._connectionsDate)
                {
                    connectionsDate.Add(date);
                }
                IGameEntity gameEntity = GameEntity.Load(gameIO._id, levelEntity, VOEnergy.Load(gameIO._energy), connectionsDate);
                return gameEntity;
            }
            throw new AppException.FileNotFound("at path" + filePath);
        }
    }
}
