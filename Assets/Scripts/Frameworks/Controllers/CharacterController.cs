using UnityEngine;
using UniMediator;
using Domain.Characters.Types;
using Frameworks.Messages;
using Frameworks.Dtos;


public class CharacterController : MonoBehaviour
{
    void Start()
    {
        // créer un character
        // passer un type à termes
        // pour l'instant, appeler aussi le usecase move always ici
        Debug.Log("_________________________Character, Start()");

        Mediator.Send<ICharacterDto>(new CreateCharacterMessage(EnumCharacter.COW, EnumDirection.LEFT, (0.0, 0.0), 20.0f));
        // should be using container.messageBroker.Send(command)
    }

}
