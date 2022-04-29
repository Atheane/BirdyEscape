using System;
using Domain.Types;
using UnityEngine;

namespace Frameworks.IO
{
    [Serializable]
    public class LevelIO
    {
        public string _id;
        public int _number;
        public CharacterIO[] _characters;
        public EnumLevelState _state;

        public LevelIO(Guid id, int number, CharacterIO[] characters, EnumLevelState state)
        {
            _id = id.ToString();
            _number = number;
            _characters = characters;
            _state = state;
        }
    }
}