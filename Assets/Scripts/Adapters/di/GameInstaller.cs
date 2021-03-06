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
using Adapters.IORepository;


public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMediator>().FromInstance(FindObjectOfType<MediatorImpl>()).AsSingle();
        Container.Bind<IDomainEventDispatcher>().To<UnimediatorDomainEventDispatcher>().AsSingle();
        Container.Bind<ICharactersRepository>().FromInstance(new InMemoryCharacterRepository(new Dictionary<Guid, ICharacterEntity>())).AsSingle();
        Container.Bind<ITilesRepository>().FromInstance(new InMemoryTileRepository(new Dictionary<Guid, ITileEntity>())).AsSingle();
        Container.Bind<ILevelsRepository>().FromInstance(new InMemoryLevelRepository(new Dictionary<Guid, ILevelEntity>())).AsSingle();
        Container.Bind<IGameRepository>().To<IOGameRepository>().AsTransient();
        Container.Bind<IOGameRepository>().To<IOGameRepository>().AsTransient();

        Container.Bind<IMapper<VOCoordinates, Vector3>>().To<Vector3ToVOCoordinatesMapper>().AsSingle();
        Container.Bind<IMapper<VOPosition, Vector3>>().To<Vector3ToVOPositionMapper>().AsSingle();
        // level usecase
        Container.Bind<CreateLevel>().AsSingle();
        Container.Bind<RestartLevel>().AsSingle();
        Container.Bind<CompleteGameLevel>().AsSingle();
        // character usecase
        Container.Bind<CreateCharacter>().AsSingle();
        Container.Bind<MoveOnceCharacter>().AsSingle();
        Container.Bind<UpdateCharacterState>().AsSingle();
        Container.Bind<TurnRight>().AsSingle();
        Container.Bind<UpdateCharacterDirection>().AsSingle();
        // character usecase
        Container.Bind<GetAllCharacters>().AsSingle();
        Container.Bind<GetCharacterState>().AsSingle();
        Container.Bind<GetCharacterPositionUsecase>().AsSingle();
        // arrow usecase
        Container.Bind<AddTileArrow>().AsSingle();
        Container.Bind<UpdateTileArrowDirection>().AsSingle();
        Container.Bind<RemoveTileArrow>().AsSingle();
        // tile usecase
        Container.Bind<CreateTile>().AsSingle();
        // controllers
        Container.Bind<LevelController>().AsSingle();
    }

}