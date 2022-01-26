using Libs.Usecases;
using Usecases.Characters.Queries;
using Domain.Characters.ValueObjects;
using Domain.Characters.Repositories;
using Domain.Characters.Entities;
using UnityEngine;


public class GetCharacterPositionUsecase : IUsecase<IGetCharacterPositionQuery, VOPosition>
{
    public ICharactersRepository _charactersRepository;
    public GetCharacterPositionUsecase(ICharactersRepository charactersRepository)
    {
        _charactersRepository = charactersRepository;
    }
    public VOPosition Execute(IGetCharacterPositionQuery query)
    {
        Debug.Log("GetCharacterPositionUsecase.Execute()");
        Debug.Log("query._characterId");
        Debug.Log(query._characterId);
        ICharacterEntity characterEntity = _charactersRepository.Find(query._characterId);
        //Debug.Log("characterEntity.Id");
        //Debug.Log(characterEntity.Id);
        //Debug.Log("characterEntity.Position");
        //Debug.Log(characterEntity.Position);
        return characterEntity.Position;
    }
}
