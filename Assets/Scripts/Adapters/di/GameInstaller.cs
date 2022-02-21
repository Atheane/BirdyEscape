using Zenject;
using System;
using System.Collections.Generic;
using UniMediator;
using UnityEngine;
using Libs.Domain.DomainEvents;
using Libs.Adapters;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Usecases;
using Adapters.InMemoryRepository;
using Adapters.Unimediatr;
using Adapters.Mappers;


public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMediator>().FromInstance(FindObjectOfType<MediatorImpl>()).AsSingle();
        Container.Bind<IDomainEventDispatcher>().To<UnimediatorDomainEventDispatcher>().AsSingle();
        Container.Bind<ICharactersRepository>().FromInstance(new InMemoryCharacterRepository(new Dictionary<Guid, ICharacterEntity>())).AsSingle();
        Container.Bind<IMapper<VOCoordinates, Vector3>>().To<Vector3ToVOCoordinatesMapper>().AsSingle();
        Container.Bind<IMapper<VOPosition, Vector3>>().To<Vector3ToVOPositionMapper>().AsSingle();
        Container.Bind<CreateCharacter>().AsSingle();
        Container.Bind<MoveAlwaysCharacter>().AsSingle();
        Container.Bind<GetCharacterPositionUsecase>().AsSingle();
        Container.Bind<TurnRight>().AsSingle();
        Container.Bind<UpdateDirection>().AsSingle();
        Container.Bind<GetAllCharacters>().AsSingle();
        Container.Bind<UpdateCharacterState>().AsSingle();
        Container.Bind<CreateArrow>().AsSingle();
        Container.Bind<CreateTile>().AsSingle();
    }
}