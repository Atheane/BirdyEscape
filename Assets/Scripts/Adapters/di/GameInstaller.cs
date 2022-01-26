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
        Container.Bind<ICharactersRepository>().FromInstance(new InMemoryCharacterRepository(new Dictionary<Guid, CharacterEntity>()));
        Container.Bind<CreateCharacter>().AsSingle();
        Container.Bind<MoveAlwaysCharacterById>().AsSingle();
        Container.Bind<GetCharacterPositionById>().AsSingle();
        //to-do how to attach handlers
    }
}