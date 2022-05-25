using System;

namespace Usecases.Queries
{
    public interface IGetCharacterPositionQuery
    {
        Guid _characterId { get; }
    }
    public class GetCharacterPositionQuery : IGetCharacterPositionQuery
    {
        public Guid _characterId { get; }

        public GetCharacterPositionQuery(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}
