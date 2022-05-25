using System;

namespace Usecases.Queries
{
    public interface IGetCharacterStateQuery
    {
        Guid _characterId { get; }
    }
    public class GetCharacterStateQuery : IGetCharacterStateQuery
    {
        public Guid _characterId { get; }

        public GetCharacterStateQuery(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}

