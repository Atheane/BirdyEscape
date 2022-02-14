using System;
using System.Collections.Generic;
using Libs.Usecases;
using Domain.Repositories;
using Domain.Entities;

public class GetAllCharacters : IUsecase<IntPtr, IReadOnlyList<ICharacterEntity>>
{
    public ICharactersRepository _charactersRepository;
    public GetAllCharacters(ICharactersRepository charactersRepository)
    {
        _charactersRepository = charactersRepository;
    }
    public IReadOnlyList<ICharacterEntity> Execute(IntPtr pointer)
    {
        return _charactersRepository.GetAll();
    }
}