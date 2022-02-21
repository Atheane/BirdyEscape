using Libs.Usecases;
using Usecases.Queries;
using Domain.Types;
using Domain.Repositories;
using Domain.Entities;
using UnityEngine;


public class GetCharacterState : IUsecase<IGetCharacterStateQuery, EnumCharacterState>
{
    public ICharactersRepository _charactersRepository;
    public GetCharacterState(ICharactersRepository charactersRepository)
    {
        _charactersRepository = charactersRepository;
    }
    public EnumCharacterState Execute(IGetCharacterStateQuery query)
    {
        ICharacterEntity characterEntity = _charactersRepository.Find(query._characterId);
        return characterEntity._state;
    }
}

