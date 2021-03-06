using UniMediator;
using Domain.Types;
using Domain.Entities;
using System.Collections.Generic;


namespace Usecases.Commands
{
    public interface ICreateLevelCommand
    {
        public int _number { get; }
        public List<ICharacterEntity> _characters { get; }
        public List<ITileEntity> _tiles { get; }
        public EnumLevelState _state { get; }
    }
    public class CreateLevelCommand : IMulticastMessage, ICreateLevelCommand
    {
        public int _number { get; private set;  }
        public List<ICharacterEntity> _characters { get; private set; }
        public List<ITileEntity> _tiles { get; private set; }
        public EnumLevelState _state { get; private set;  }

        public CreateLevelCommand(
            int number,
            List<ICharacterEntity> characters,
            List<ITileEntity> tiles,
            EnumLevelState state)
        {
            _number = number;
            _characters = characters;
            _tiles = tiles;
            _state = state;
        }
    }
}
