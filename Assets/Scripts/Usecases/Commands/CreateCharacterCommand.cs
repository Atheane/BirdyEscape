using UniMediator;
using Domain.Types;
using UnityEngine;

namespace Usecases.Commands
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacterType _type { get; }
        public EnumDirection _direction { get; }
        public Vector3 _position { get; }
        public int _speed { get; }
    }
    public class CreateCharacterCommand : ISingleMessage<string>, ICreateCharacterCommand
    {
        public EnumCharacterType _type { get; private set; }
        public EnumDirection _direction { get; private set; }
        public Vector3 _position { get; private set; }
        public int _speed { get; private set; }

        public CreateCharacterCommand(EnumCharacterType type, EnumDirection direction, Vector3 position, int speed)
        {
            _type = type;
            _direction = direction;
            _position = position;
            _speed = speed;
        }
    }
}
