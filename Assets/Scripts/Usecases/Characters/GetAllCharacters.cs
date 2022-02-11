using System;
using System.Collections.Generic;
using Libs.Usecases;
using Domain.Characters.Repositories;

public class GetAllCharactersIds : IUsecase<IntPtr, IReadOnlyList<Guid>>
{
    public ICharactersRepository _charactersRepository;
    public GetAllCharactersIds(ICharactersRepository charactersRepository)
    {
        _charactersRepository = charactersRepository;
    }
    public IReadOnlyList<Guid> Execute(IntPtr pointer)
    {
        List<Guid> guidList = new List<Guid>();
        foreach (var character in _charactersRepository.GetAll())
        {
            guidList.Add(character.Id);
        }
        return guidList.AsReadOnly();
    }
}
