using Zenject;
using System;
using System.Collections.Generic;
using UniMediator;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;
using Usecases;
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
        Container.Bind<UpdateDirection>().AsSingle();
        Container.Bind<GetAllCharacters>().AsSingle();
        Container.Bind<CreateArrow>().AsSingle();
    }
}