using Libs.Usecases;
using Usecases.Characters.Queries;
using Domain.Characters.ValueObjects;
using Domain.Characters.Repositories;
using Domain.Characters.Entities;


public class GetCharacterPositionUsecase : IUsecase<IGetCharacterPositionQuery, VOPosition>
{
    public ICharactersRepository _charactersRepository;
    public GetCharacterPositionUsecase(ICharactersRepository charactersRepository)
    {
        _charactersRepository = charactersRepository;
    }
    public VOPosition Execute(IGetCharacterPositionQuery query)
    {
        ICharacterEntity characterEntity = _charactersRepository.Find(query._characterId);
        return characterEntity.Position;
    }
}
