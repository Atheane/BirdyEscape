using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Domain.Characters.Types;
using UniMediator;

public class Flowers : MonoBehaviour
{
    //CreateCharacterHandler _handler;

    //public Flowers(CreateCharacterHandler handler)
    //{
    //    _handler = handler;
    //}

    void Start()
    {
        Debug.Log("_____________________Flowers > Start()");
        Mediator.AddMediatedObject(this);
        //EventManager.current.characterCreated += HandleCharacterCreated;
    }

    private void HandleCharacterCreated(EnumCharacter characterType)
    {
        Debug.Log("CHARACTER_CREATED");
        Debug.Log(characterType);
    }

    //private void OnDestroy()
    //{
    //    EventManager.current.characterCreated -= HandleCharacterCreated;
    //}
}
