using System;
using UnityEngine;
using Zenject;
using Domain.Commons.Types;
using Usecases.Characters;
using Usecases.Characters.Commands;

public class ArrowController : MonoBehaviour
{
    public EnumDirection _direction;
    private DiContainer _container;
    public LayerMask _layer;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnumDirection direction = other.GetComponent<CharacterMoveController>()._direction;
        if (direction != _direction)
        {
            Guid id = other.GetComponent<CharacterMoveController>()._id;
            _container.Resolve<UpdateDirection>().Execute(new UpdateDirectionCommand(id, _direction));
        }
    }

}
