using UnityEngine;
using UniMediator;
using Domain.Characters.Types;


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
    }


}
