using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;
using Domain.Characters.ValueObjects;

public class CharacterMoveController : MonoBehaviour
{
    private Guid _id;
    GetCharacterPositionById _usecase;

    [Inject]
    public void Construct(GetCharacterPositionById usecase)
    {
        _usecase = usecase;
    }

    //public void Construct(GetCharacterPositionById usecase)
    //{
    //    _usecase = usecase;
    //}

    // Start is called before the first frame update
    void Start()
    {
        _id = CreateCharacterHandler.GetCharacterId();
        Debug.Log("CharacterController Start");
        Debug.Log("_id");
        Debug.Log(_id);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CharacterController Update");
        VOPosition position = _usecase.Execute(_id);
        Debug.Log("position");
        Debug.Log(position);
    }

    public void SetId(Guid id)
    {
        _id = id;
    }

    //public void DrawCharacter(ICharacterDto characterDto)
    //{
    //    Transform grid = this.transform;
    //    GameObject cow = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
    //    cow.tag = characterDto.Image;
    //    cow.transform.parent = grid;
    //}
}
