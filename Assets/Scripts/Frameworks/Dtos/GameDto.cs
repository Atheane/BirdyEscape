using System;
using System.Collections.Generic;
using Domain.Entities;
using Frameworks.Dtos;

namespace Frameworks.Dtos
{
    public interface IGameDto
    {
        public Guid _id { get; }
        public LevelDto _currentLevel { get; }
        public float _energy { get; }
        public List<DateTime> _connectionsDate { get; }
    }

    [Serializable]
    public class GameDto : IGameDto
    {
        public Guid _id { get; private set; }
        public LevelDto _currentLevel { get; private set; }
        public float _energy { get; private set; }
        public List<DateTime> _connectionsDate { get; private set; }

        public GameDto(Guid id, ILevelEntity currentLevel, float energy, List<DateTime> connectionsDate)
        {
            _id = id;
            _currentLevel = LevelDto.Create(currentLevel._id, currentLevel._number, currentLevel._characters, currentLevel._tiles, currentLevel._state);
            _energy = energy;
            _connectionsDate = connectionsDate;
        }
    }
}