using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using Usecases.Characters;
using Usecases.Characters.Commands;

public class PlayButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private DiContainer _container;
    private IReadOnlyList<Guid> _characterIds;
    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        _characterIds = _container.Resolve<GetAllCharactersIds>().Execute(IntPtr.Zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("POINTER DOWN");
        foreach (Guid id in _characterIds)
        {
            Debug.Log(id);
            _container.Resolve<MoveAlwaysCharacter>().Execute(new MoveAlwaysCharacterCommand(id));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("POINTER UP");
    }


}
