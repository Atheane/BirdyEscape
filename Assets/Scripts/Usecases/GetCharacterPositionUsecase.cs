using Libs.Usecases;
using Usecases.Queries;
using Domain.ValueObjects;
using Domain.Repositories;
using Domain.Entities;
using UnityEngine;


public class GetCharacterPositionUsecase : IUsecase<IGetCharacterPositionQuery, VOPosition3D>
{
    public ICharactersRepository _charactersRepository;
    public GetCharacterPositionUsecase(ICharactersRepository charactersRepository)
    {
        _charactersRepository = charactersRepository;
    }
    public VOPosition3D Execute(IGetCharacterPositionQuery query)
    {
        ICharacterEntity characterEntity = _charactersRepository.Find(query._characterId);
        return characterEntity._position;
    }
}
