using Zenject;
using System;
using System.Collections.Generic;
using UniMediator;
using Libs.Domain.DomainEvents;
using Domain.Characters.Entities;
using Domain.Characters.Repositories;
using Usecases.Characters;
using Adapters.InMemoryRepository;
using Adapters.Unimediatr;


public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMediator>().FromInstance(FindObjectOfType<MediatorImpl>());
        Container.Bind<IDomainEventDispatcher>().To<UnimediatorDomainEventDispatcher>().AsSingle();
        Container.Bind<ICharactersRepository>().FromInstance(new InMemoryCharacterRepository(new Dictionary<Guid, ICharacterEntity>()));
        Container.Bind<CreateCharacter>().AsSingle();
        Container.Bind<MoveAlwaysCharacter>().AsSingle();
        Container.Bind<GetCharacterPositionUsecase>().AsSingle();
        Container.Bind<TurnRight>().AsSingle();
        //to-do how to attach handlers
    }
}