using UnityEngine;
using Zenject;
using Domain.Characters.Types;
using Domain.Characters.Constants;
using Usecases.Characters;
using Usecases.Characters.Commands;


public class GridController : MonoBehaviour
{
    private DiContainer _container;
    public float INIT_X;
    public float INIT_Y;
    public float INIT_Z;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        (float, float, float) INIT_POSITION = (Position.INIT_X, Position.INIT_Y, Position.INIT_Z);
        if (INIT_X != 0.0f && INIT_Y != 0.0f && INIT_Z != 0.0f)
        {
            INIT_POSITION = (INIT_X, INIT_Y, INIT_Z);
        } 
        _container.Resolve<CreateCharacter>().Execute(new CreateCharacterCommand(EnumCharacterType.BLACK_BIRD, EnumCharacterDirection.LEFT, INIT_POSITION, Speed.INIT_SPEED));
    }

}

