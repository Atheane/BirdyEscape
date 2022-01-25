using UnityEngine;
using UniMediator;
using Zenject;
using Domain.Characters.Types;
using Usecases.Characters;
using Frameworks.Messages;

public class MoveCharacterByIdHandler : MonoBehaviour,
ISingleMessageHandler<MoveCharacterByIdMessage, EnumCharacterState>
{
    private MoveCharacterById _usecase;

    [Inject]
    public void Construct(MoveCharacterById usecase)
    {
        Debug.Log("new MoveCharacterByIdHandler()");
        _usecase = usecase;
        Debug.Log("_usecase injected");
    }

    public EnumCharacterState Handle(MoveCharacterByIdMessage message)
    {
        var characterState = _usecase.Execute(message);

        // Map Character entty to DTO (with image source)
        this.MoveCharacter();
        return characterState;
    }
    public void MoveCharacter()
    {
        Transform grid = this.transform;
        GameObject cow = this.transform.gameObject;
        cow.transform.parent = grid;
    }
}