using System;
using System.Collections.Generic;

namespace Frameworks.Dtos
{
    public interface ISimpleGameDto
    {
        public Guid _id { get; }
        public int _currentLevel { get; }
        public float _energy { get; }
        public List<DateTime> _connectionsDate { get; }
    }

    [Serializable]
    public class SimpleGameDto : ISimpleGameDto
    {
        public Guid _id { get; private set; }
        public int _currentLevel { get; private set; }
        public float _energy { get; private set; }
        public List<DateTime> _connectionsDate { get; private set; }

        public SimpleGameDto(Guid id, int currentLevel, float energy, List<DateTime> connectionsDate)
        {
            _id = id;
            _currentLevel = currentLevel;
            _energy = energy;
            _connectionsDate = connectionsDate;
        }
    }
}