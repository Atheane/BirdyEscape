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
        Mediator.Send<ICharacterDto>(new CreateCharacterMessage(EnumCharacter.COW, EnumDirection.LEFT, (Position.INIT_X, Position.INIT_Y), Speed.INIT_SPEED));
        // should be using container.messageBroker.Send(command)
        Mediator.Send<ICharacterDto>(new CreateCharacterMessage(EnumCharacter.COW, EnumDirection.LEFT, (Position.INIT_X, Position.INIT_Y), Speed.INIT_SPEED));

    }

}
