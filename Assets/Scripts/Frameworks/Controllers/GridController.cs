using UnityEngine;
using UniMediator;
using Zenject;
using Domain.Characters.Types;
using Domain.Characters.Constants;
using Frameworks.Messages;
using Frameworks.Dtos;


public class GridController : MonoBehaviour
{
    private IMediator _mediator;

    [Inject]
    public void Construct(IMediator mediator)
    {
        _mediator = mediator;
    }

    void Start()
    {
        Debug.Log("________GridController, Start()");
        ICharacterDto cow = _mediator.Send<ICharacterDto>(new CreateCharacterMessage(EnumCharacterType.COW, EnumCharacterDirection.LEFT, (Position.INIT_X, Position.INIT_Y), Speed.INIT_SPEED));
        Debug.Log("Created cow");
        Debug.Log(cow);
        EnumCharacterState cowState = _mediator.Send<EnumCharacterState>(new MoveCharacterByIdMessage(cow.Id));
        Debug.Log("cow new state");
        Debug.Log(cowState);
    }

}
