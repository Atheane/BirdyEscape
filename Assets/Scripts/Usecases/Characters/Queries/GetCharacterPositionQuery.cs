using System;
using UniMediator;
using Domain.Characters.ValueObjects;

namespace Usecases.Characters.Queries
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
