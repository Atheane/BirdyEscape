using UnityEngine;
using Zenject;
using Domain.Characters.Types;
using Domain.Characters.Constants;
using Usecases.Characters;
using Usecases.Characters.Commands;


public class GridController : MonoBehaviour
{
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        _container.Resolve<CreateCharacter>().Execute(new CreateCharacterCommand(EnumCharacterType.COW, EnumCharacterDirection.LEFT, (Position.INIT_X, Position.INIT_Y), Speed.INIT_SPEED));
    }

}

