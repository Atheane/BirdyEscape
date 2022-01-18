using UnityEngine;
using UniMediator;
using Domain.Characters.Types;
using Domain.Characters.DomainEvents;
using Frameworks.Commands;


public class CreateCharacterHandler : MonoBehaviour,
ISingleMessageHandler<CreateCharacterCommand, EnumCharacter>
{
    public EnumCharacter Handle(CreateCharacterCommand command)
    {
        Debug.Log(">>> IN COMMAND");
        Debug.Log(command);
        // map command to domain event
        var domainEvent = new CharacterCreatedDomainEvent(command.Message);
        Debug.Log(domainEvent);
        // invoke adapter handler
        // return saved Entity Character
        // Map Character to DTO (with image source)
        return command.Message;
    }
}


