using UniMediator;
using Domain.Characters.Types;

namespace Frameworks.Commands
{
    public class CreateCharacterCommand : ISingleMessage<EnumCharacter>
    {
        public EnumCharacter Message { get; set; }

        public CreateCharacterCommand(EnumCharacter command)
        {
            Message = command;
        }
    }
}
