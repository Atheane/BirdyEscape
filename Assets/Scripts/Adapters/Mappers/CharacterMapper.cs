using Libs.Adapters;
using Frameworks.Messages;
using Usecases.Characters;

public class CharacterMapper: IMapper<ICreateCharacterCommand, CreateCharacterMessage>
{
    public ICreateCharacterCommand ToDomain(CreateCharacterMessage message)
    {
        return (ICreateCharacterCommand)message;
    }
}
