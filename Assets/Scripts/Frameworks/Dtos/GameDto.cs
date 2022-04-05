using System;


namespace Frameworks.Dtos
{
    public interface IGameDto
    {
        public Guid _id { get; }
        public int _currentLevel { get; }
        public float _energy { get; }
        public DateTime _firstConnectionDate { get; }
    }

    [Serializable]
    public class GameDto : IGameDto
    {
        public Guid _id { get; private set; }
        public int _currentLevel { get; private set; }
        public float _energy { get; private set; }
        public DateTime _firstConnectionDate { get; private set; }

        public GameDto(Guid id, int currentLevel, float energy, DateTime firstConnectionDate)
        {
            _id = id;
            _currentLevel = currentLevel;
            _energy = energy;
            _firstConnectionDate = firstConnectionDate;
        }
    }
}