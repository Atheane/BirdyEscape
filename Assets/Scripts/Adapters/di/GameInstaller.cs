using Zenject;
using System;
using System.Collections.Generic;
using Domain.Characters.Entities;
using Domain.Characters.Repositories;
using Adapters.InMemoryRepository;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ICharactersRepository>().FromInstance(new InMemoryCharacterRepository(new Dictionary<Guid, CharacterEntity>()));
    }
}