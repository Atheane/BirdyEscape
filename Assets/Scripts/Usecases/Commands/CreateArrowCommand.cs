using System;
using UniMediator;
using Domain.Types;
using UnityEngine;

namespace Usecases.Commands
{
    public interface ICreateArrowCommand
    {
        public Guid _tileId { get; }
        public EnumDirection _direction { get; }
        public Vector3 _position { get; }
        public string _path { get;  }
    }
    public class CreateArrowCommand : IMulticastMessage, ICreateArrowCommand
    {
        public Guid _tileId { get; private set; }
        public EnumDirection _direction { get; private set; }
        public Vector3 _position { get; private set; }
        public string _path { get; private set; }

        public CreateArrowCommand(Guid tileId, EnumDirection direction, Vector3 position, string path)
        {
            _tileId = tileId;
            _direction = direction;
            _position = position;
            _path = path;
        }
    }
}
