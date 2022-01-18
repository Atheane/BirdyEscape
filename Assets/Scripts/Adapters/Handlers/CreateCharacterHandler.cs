using UnityEngine;
using UniMediator;
using Domain.Characters.Types;
using Domain.Characters.DomainEvents;
using Adapters.Commands;

namespace Adapters.Handlers
{
    public class CreateCharacterHandler : MonoBehaviour,
    ISingleMessageHandler<CreateCharacterCommand, EnumCharacter>
    {
        public EnumCharacter Handle(CreateCharacterCommand command)
        {
            Debug.Log(">>> IN COMMAND");
            Debug.Log(command);
            Debug.Log(">>> OUT DOMAIN EVENT");
            var domainEvent = new CharacterCreatedDomainEvent(command.Message);
            Debug.Log(domainEvent);
            // map command to domain event
            // Usecase CreateCharacter
            // create Domain Entity Character
            // save Entity Character in Repository
            // return saved Entity Character
            // Map Character to DTO (with image source)
            return command.Message;
        }
    }
}


