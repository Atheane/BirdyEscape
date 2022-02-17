using System;
using UniMediator;
using System.Collections.Generic;

namespace Usecases.Characters.Queries
{
    public interface IGetAllCharactersQuery
    {
    }
    public class GetAllCharactersQuery : ISingleMessage<IReadOnlyList<Guid>>, IGetAllCharactersQuery
    {
        public GetAllCharactersQuery()
        {
        }
    }
}