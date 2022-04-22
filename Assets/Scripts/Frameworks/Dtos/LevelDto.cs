using System;
using System.Collections.Generic;
using Domain.Types;
using Domain.Entities;

namespace Frameworks.Dtos
{
    public interface ILevelDto
    {
        public Guid _id { get; }
        public int _number { get; }
        public ICharacterDto[] _characters { get; }
        public ITileDto[] _tiles { get; }
        public EnumLevelState _state { get; }
    }

    public class LevelDto : ILevelDto
    {
        public Guid _id { get; private set; }
        public int _number { get; private set; }
        public ICharacterDto[] _characters { get; private set; }
        public ITileDto[] _tiles { get; private set; }
        public EnumLevelState _state { get; private set; }

        private LevelDto(Guid id, int number, ICharacterDto[] characters, ITileDto[] tiles, EnumLevelState state)
        {
            _id = id;
            _number = number;
            _characters = characters;
            _tiles = tiles;
            _state = state;
        }

        public static LevelDto Create(Guid id, int number, ICharacterEntity[] characters, ITileEntity[] tiles, EnumLevelState state)
        {
            List<ICharacterDto> characterDtos = new List<ICharacterDto>();
            foreach (ICharacterEntity character in characters)
            {
                characterDtos.Add(CharacterDto.Create(character._id, character._type, character._direction, character._state, character._position, character._speed, character._totalDistance));
            }

            List<ITileDto> tileDtos = new List<ITileDto>();
            foreach (ITileEntity tile in tiles)
            {
                tileDtos.Add(TileDto.Create(tile._id, tile._coordinates, tile._path));
            }
            return new LevelDto(id, number, characterDtos.ToArray(), tileDtos.ToArray(), state);
        }

    }
}