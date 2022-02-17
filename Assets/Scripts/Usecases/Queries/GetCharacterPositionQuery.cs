using System;
using UniMediator;
using Domain.ValueObjects;

namespace Usecases.Queries
{
    public interface IGetCharacterPositionQuery
    {
        Guid _characterId { get; }
    }
    public class GetCharacterPositionQuery : ISingleMessage<VOPosition>, IGetCharacterPositionQuery
    {
        public Guid _characterId { get; }

        public GetCharacterPositionQuery(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}
