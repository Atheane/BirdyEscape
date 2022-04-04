using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum MainMenuButtonState { CLICKED, IDLE };

public class MainMenuController : MonoBehaviour, IPointerDownHandler
{
    public MainMenuButtonState _state;

    void Start()
    {
        _state = MainMenuButtonState.IDLE;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_state == MainMenuButtonState.IDLE)
        {
            _state = MainMenuButtonState.CLICKED;
            //to-do load last game statistics
            //Game AggregateRoot. Has currentLevel, energy, firstConnection, player
            var lastlevelNumber = 1;
            SceneManager.LoadScene("Level" + lastlevelNumber, LoadSceneMode.Single);
        }
    }
}
