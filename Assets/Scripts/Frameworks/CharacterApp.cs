using UnityEngine;
using UniMediator;
using Domain.Characters.Types;


public class CharacterApp : MonoBehaviour
{

    void Start()
    {
        // créer un character
        // passer un type à termes
        // pour l'instant, appeler aussi le usecase move always ici
        Debug.Log("_________________________Character, Start()");
        var message = new CreateCharacterMessage("CREATE_CHARACTER");
        Mediator.Send<string>(message);
        //EventManager.current.CreateCharacter(EnumCharacter.COW);
    }


}
