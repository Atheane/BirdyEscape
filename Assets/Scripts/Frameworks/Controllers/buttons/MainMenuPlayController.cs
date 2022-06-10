using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;
using Adapters.IORepository;
using Frameworks.IO;

public enum MainMenuPlayButtonState { CLICKED, IDLE };

public class MainMenuPlayController : MonoBehaviour, IPointerDownHandler
{
    public MainMenuPlayButtonState _state;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        _state = MainMenuPlayButtonState.IDLE;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_state == MainMenuPlayButtonState.IDLE)
        {
            _state = MainMenuPlayButtonState.CLICKED;
            try
            {
                GameIO gameIO = _container.Resolve<IOGameRepository>().PreLoad();
                SceneManager.LoadScene("Level" + gameIO._currentLevel._number, LoadSceneMode.Single);
            } catch(Exception e)
            {
                Debug.Log(e);
                Debug.Log("_________________________11111111111 should be HERRREE");
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            }
        }
    }
}
