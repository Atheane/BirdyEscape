using UnityEngine;
using UniMediator;
using Domain.Characters.Types;
using Domain.Characters.Constants;
using Frameworks.Messages;
using Frameworks.Dtos;


public class GridController : MonoBehaviour
{
    void Start()
    {
        Debug.Log("________GridController, Start()");
        ICharacterDto cow = Mediator.Send<ICharacterDto>(new CreateCharacterMessage(EnumCharacterType.COW, EnumCharacterDirection.LEFT, (Position.INIT_X, Position.INIT_Y), Speed.INIT_SPEED));
        // should be using container.messageBroker.Send(command)
        Debug.Log("Created cow");
        Debug.Log(cow);
        EnumCharacterState cowState = Mediator.Send<EnumCharacterState>(new MoveCharacterByIdMessage(cow.Id));
        Debug.Log("cow new state");
        Debug.Log(cowState);
    }

}
