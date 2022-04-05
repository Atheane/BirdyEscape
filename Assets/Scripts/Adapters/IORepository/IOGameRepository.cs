using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Domain.Repositories;
using Domain.Entities;

public class IOGameRepository : IGameRepository
{
    public void Save(IGameEntity gameEntity)
    {
        string destination = Application.dataPath + "/Game.txt";
        Debug.Log("______________________ SAVE");
        Debug.Log(Application.dataPath);
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        IGameEntity data = GameEntity.Create(gameEntity._currentLevel, gameEntity._energy);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public IGameEntity Load()
    {
        string destination = Application.dataPath + "/Game.txt";
        FileStream file;
        Debug.Log("______________________ LOAD");
        Debug.Log(Application.dataPath);
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            throw new System.Exception("File not found");
        }
        BinaryFormatter bf = new BinaryFormatter();
        IGameEntity data = (IGameEntity)bf.Deserialize(file);
        file.Close();
        return data;
    }
}
