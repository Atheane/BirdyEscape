using System;
using UnityEngine;
using Domain.Characters.Types;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    public event Action<EnumCharacter> characterCreated;
    public void CreateCharacter(EnumCharacter characterType)
    {
        if (characterCreated != null)
        {
            characterCreated(characterType);
        }
    }

}
