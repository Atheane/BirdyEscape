using UnityEngine;
using UniMediator;

public class CreateCharacterHandler : MonoBehaviour,
    ISingleMessageHandler<CreateCharacterMessage, string>
{
    public string Handle(CreateCharacterMessage message)
    {
        Debug.Log(message.Message + "Pong");
        return message.Message + "Pong";
    }
}