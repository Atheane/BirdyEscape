using System;
using System.Collections.Generic;
using Domain.Types;
using Domain.Entities;

namespace Frameworks.Dtos
{
    [Serializable]
    public class LevelDto
    {
        public string _id;
        public int _number;
        public CharacterDto[] _characters;
        public TileDto[] _tiles { get; private set; }
        public EnumLevelState _state;

        private LevelDto(Guid id, int number, CharacterDto[] characters, TileDto[] tiles, EnumLevelState state)
        {
            _id = id.ToString();
            _number = number;
            _characters = characters;
            _tiles = tiles;
            _state = state;
        }

        public static LevelDto Create(Guid id, int number, ICharacterEntity[] characters, ITileEntity[] tiles, EnumLevelState state)
        {
            List<CharacterDto> characterDtos = new List<CharacterDto>();
            foreach (ICharacterEntity character in characters)
            {
                characterDtos.Add(CharacterDto.Create(character._id, character._type, character._direction, character._state, character._position, character._speed, character._totalDistance));
            }

            List<TileDto> tileDtos = new List<TileDto>();
            foreach (ITileEntity tile in tiles)
            {
                tileDtos.Add(TileDto.Create(tile._id, tile._coordinates, tile._path));
            }
            return new LevelDto(id, number, characterDtos.ToArray(), tileDtos.ToArray(), state);
        }

    }
}