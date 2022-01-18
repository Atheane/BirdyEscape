using System;
using Libs.Usecases;
using Domain.Characters.Repositories;
using Domain.Characters.Entities;
using Domain.Characters.Types;
using Domain.Characters.ValueObjects;

public interface ICreateCharacterCommand: ICharacterEntity
{
    new Guid Id { get; }
    new EnumCharacter Type { get; }
    new EnumDirection Direction { get; }
    new VOPosition Position { get; }
    new float Speed { get; }
}

public class CreateCharacter : IUsecase<ICreateCharacterCommand, CharacterEntity>
{
    public ICharactersRepository charactersRepository;
    public CreateCharacter(ICharactersRepository charactersRepository)
    {
        this.charactersRepository = charactersRepository;
    }
    public CharacterEntity Execute(ICreateCharacterCommand command)
    {
        var character = CharacterEntity.Create(command.Type, command.Direction, command.Position, command.Speed);
        this.charactersRepository.Add(character);
        character.MoveAlways();
        return character;
    }
}