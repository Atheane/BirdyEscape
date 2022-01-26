using UnityEngine;
using UniMediator;
using Zenject;
using Usecases.Characters;
using Domain.Characters.ValueObjects;
using Frameworks.Messages;

public class MoveAlwaysCharacterByIdHandler : MonoBehaviour,
ISingleMessageHandler<MoveAlwaysCharacterByIdMessage, VOPosition>
{
    private MoveAlwaysCharacterById _usecase;

    [Inject]
    public void Construct(MoveAlwaysCharacterById usecase)
    {
        _usecase = usecase;
    }

    public VOPosition Handle(MoveAlwaysCharacterByIdMessage message)
    {
        var position = _usecase.Execute(message);
        MoveCharacter(new Vector3(position.Value.X, position.Value.Y, 0));
        return position;
    }

    public void MoveCharacter(Vector3 position)
    {
        GameObject cow = GameObject.FindGameObjectWithTag("Cow");
        cow.transform.position = position;
    }
}