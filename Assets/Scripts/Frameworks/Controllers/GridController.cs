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

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        (float, float) INIT_POSITION = (Position.INIT_X, Position.INIT_Y);
        if (INIT_X != 0.0f && INIT_Y != 0.0f)
        {
            INIT_POSITION = (INIT_X, INIT_Y);
        } 
        _container.Resolve<CreateCharacter>().Execute(new CreateCharacterCommand(EnumCharacterType.COW, EnumCharacterDirection.LEFT, INIT_POSITION, Speed.INIT_SPEED));
    }

}

