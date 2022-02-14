using System;
using UniMediator;
using Domain.ValueObjects;

namespace Usecases.Queries
{
    public interface IGetCharacterPositionQuery
    {
        Guid _characterId { get; }
    }
    public class GetCharacterPositionQuery : ISingleMessage<VOPosition3D>, IGetCharacterPositionQuery
    {
        public Guid _characterId { get; }

        public GetCharacterPositionQuery(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}
