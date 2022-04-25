using System;
using System.Collections.Generic;
using Domain.Entities;
using UnityEngine;

namespace Frameworks.Dtos
{
    [Serializable]
    public class GameDto
    {
        public Guid _id;
        public LevelDto _currentLevel;
        public float _energy;
        public List<DateTime> _connectionsDate;

        public GameDto(Guid id, ILevelEntity currentLevel, float energy, List<DateTime> connectionsDate)
        {
            _id = id;
            _currentLevel = LevelDto.Create(currentLevel._id, currentLevel._number, currentLevel._characters, currentLevel._tiles, currentLevel._state);
            _energy = energy;
            _connectionsDate = connectionsDate;
        }
    }
}