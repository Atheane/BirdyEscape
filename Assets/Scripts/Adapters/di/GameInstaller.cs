using Zenject;
using System;
using System.Collections.Generic;
using UniMediator;
using Domain.Characters.Entities;
using Domain.Characters.Repositories;
using Adapters.InMemoryRepository;
using Usecases.Characters;


public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMediator>().To<MediatorImpl>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ICharactersRepository>().FromInstance(new InMemoryCharacterRepository(new Dictionary<Guid, ICharacterEntity>()));
        Container.Bind<CreateCharacter>().AsSingle();
        Container.Bind<MoveAlwaysCharacter>().AsSingle();
        Container.Bind<GetCharacterPositionUsecase>().AsSingle();
        Container.Bind<ChangeCharacterDirection>().AsSingle();
        //to-do how to attach handlers
    }
}