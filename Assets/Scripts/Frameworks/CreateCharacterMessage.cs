using UniMediator;

public class CreateCharacterMessage : ISingleMessage<string>
{
    public string Message { get; set; }

    public CreateCharacterMessage(string message)
    {
        Message = message;
    }
}