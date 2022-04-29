using System;
using System.Collections.Generic;
using Domain.Entities;
using UnityEngine;

namespace Frameworks.IO
{
    [Serializable]
    public class GameIO
    {
        public string _id;
        public LevelIO _currentLevel;
        public float _energy;
        public List<JsonDateTime> _connectionsDate;

        public GameIO(Guid id, ILevelEntity currentLevel, float energy, List<DateTime> dates)
        {
            var charactersIO = new List<CharacterIO>();
            foreach (ICharacterEntity characterEntity in currentLevel._characters)
            {
                charactersIO.Add(new CharacterIO(characterEntity._id, characterEntity._totalDistance));
            }
            _id = id.ToString();
            _currentLevel = new LevelIO(currentLevel._id, currentLevel._number, charactersIO.ToArray(), currentLevel._state);
            _energy = energy;

            var connectionsDate = new List<JsonDateTime>();
            foreach (DateTime date in dates)
            {
                connectionsDate.Add(date);
            }
            _connectionsDate = connectionsDate;
        }
    }
}