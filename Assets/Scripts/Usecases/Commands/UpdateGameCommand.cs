using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IUpdateGameCommand
    {
        public int _currentLevel { get; }
        public float _energy { get; }
    }
    public class UpdateGameCommand : IMulticastMessage, IUpdateGameCommand
    {
        public int _currentLevel { get; private set; }
        public float _energy { get; private set; }


        public UpdateGameCommand(int currentLevel, float energy)
        {
            _currentLevel = currentLevel;
            _energy = energy;
        }
    }
}
