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
        _usecase = usecase;
    }

    public EnumCharacterState Handle(MoveCharacterByIdMessage message)
    {
        var characterState = _usecase.Execute(message);

        MoveCharacter();
        return characterState;
    }
    public void MoveCharacter()
    {
        Transform grid = this.transform;
        GameObject cow = this.transform.gameObject;
        cow.transform.parent = grid;
    }
}