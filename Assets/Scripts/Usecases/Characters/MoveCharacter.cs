using System;
using Libs.Usecases;
using Domain.Characters.Repositories;
using Domain.Characters.Entities;

public interface IMoveCharacterByIdCommand
{
    Guid CharacterId { get; }
}

public class MoveCharacterById : IUsecase<IMoveCharacterByIdCommand, CharacterEntity>
{
    public ICharactersRepository charactersRepository;
    public MoveCharacterById(ICharactersRepository charactersRepository)
    {
        this.charactersRepository = charactersRepository;
    }
    public CharacterEntity Execute(IMoveCharacterByIdCommand command)
    {
        var character = this.charactersRepository.Find(command.CharacterId);
        character.MoveAlways();
        return character;
    }
}
