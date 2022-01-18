using UnityEngine;
using UniMediator;
using Domain.Characters.Types;
using Frameworks.Commands;


public class CharacterController : MonoBehaviour
{
    void Start()
    {
        // créer un character
        // passer un type à termes
        // pour l'instant, appeler aussi le usecase move always ici
        Debug.Log("_________________________Character, Start()");
        var command = new CreateCharacterCommand(EnumCharacter.COW);

        Mediator.Send<EnumCharacter>(command);
        // should be using container.messageBroker.Send(command)
    }

}
