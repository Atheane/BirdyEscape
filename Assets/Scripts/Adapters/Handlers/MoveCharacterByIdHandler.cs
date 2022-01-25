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

        MoveCharacter(new Vector3(-10f, 0.7f, 0f));
        return characterState;
    }

    public void MoveCharacter(Vector3 position)
    {
        GameObject cow = GameObject.FindGameObjectWithTag("Cow");
        cow.transform.position = position;
    }
}