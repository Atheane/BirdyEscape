using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;

public class CharacterMoveController : MonoBehaviour
{
    private Guid _id;
    private MoveAlwaysCharacterById _usecase;

    [Inject]
    public void Construct(MoveAlwaysCharacterById usecase)
    {
        _usecase = usecase;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CharacterController Update");
        Vector3 _cowPosition = transform.position;
        Debug.Log(_cowPosition);
        //VOPosition position = _usecase.Execute(_id);
        //Debug.Log("position");
        //Debug.Log(position);
    }

}
