using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;
using Usecases.Characters.Commands;

public class CharacterMoveController : MonoBehaviour
{
    private Guid _id;
    private DiContainer _container;

    //private MoveAlwaysCharacterById _usecase;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    //[Inject]
    //public void Construct(MoveAlwaysCharacterById usecase)
    //{
    //    _usecase = usecase;
    //}

    // Update is called once per frame
    void Update()
    {
        var character = _container.Resolve<MoveAlwaysCharacterById>().Execute(new MoveAlwaysCharacterByIdCommand(_id));

        Debug.Log("CharacterController Update");
        Vector3 _cowPosition = transform.position;
        Debug.Log(_cowPosition);
        //VOPosition position = _usecase.Execute(_id);
        //Debug.Log("position");
        //Debug.Log(position);
    }

}
