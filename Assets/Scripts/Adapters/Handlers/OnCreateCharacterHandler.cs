using Domain.Characters.Entities;
using Usecases.Characters;
using Adapters.Commands;


// Adapters.Handlers are responsible for executing a usecase

namespace Adapters.Handlers
{
    public class OnCreateCharacterHandler
    {
        public CreateCharacter Usecase;
        public OnCreateCharacterHandler(CreateCharacter usecase)
        {
            Usecase = usecase;
        }
        public ICharacterEntity Handle(ICreateCharacterCommand command)
        {
            return this.Usecase.Execute(command);
        }

    }
}
