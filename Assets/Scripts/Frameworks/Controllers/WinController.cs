using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;

public enum WinState { SHOWN, HIDDEN };

public class WinController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameLevelCompleted>>
{
    public WinState _state;
    private Slider _slider;
    private string _text;

    // Start is called before the first frame update
    private void Start()
    {
        _state = WinState.HIDDEN;
        gameObject.SetActive(false);
        _slider = gameObject.GetComponentInChildren<Slider>();
        Debug.Log(GameObject.FindWithTag("RemainingEnergy"));
    }

    public void Handle(DomainEventNotification<GameLevelCompleted> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = WinState.SHOWN;
        gameObject.SetActive(true);
        var energy = notification._domainEvent._props._energy.Value;
        _slider.value = energy / 100;
        _text = Mathf.RoundToInt(energy).ToString() + "/ 100";
        //SceneManager.LoadScene("Level" + nextLevelNumber, LoadSceneMode.Single);
    }
}
