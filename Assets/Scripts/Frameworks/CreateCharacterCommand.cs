using UniMediator;
using Domain.Characters.Types;

public class CreateCharacterCommand : ISingleMessage<EnumCharacter>
{
    public EnumCharacter Message { get; set; }

    public CreateCharacterCommand(EnumCharacter command)
    {
        Message = command;
    }
}