using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Characters.ValueObjects;
using Domain.Characters.Types;
using Domain.Characters.Entities;


namespace Usecases.Characters
{
    public class MoveAlwaysCharacter : IUsecase<IMoveAlwaysCharacterCommand, VOPosition>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public ICharacterEntity _characterEntity;

        public MoveAlwaysCharacter(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public VOPosition Execute(IMoveAlwaysCharacterCommand command)
        {
            _characterEntity = _charactersRepository.Find(command._characterId);
            _characterEntity.UpdateState(EnumCharacterState.MOVING);
            _charactersRepository.Update(_characterEntity);
            _domainEventDispatcher.Dispatch(_characterEntity);
            MoveAlways(_characterEntity.Speed);
            return _characterEntity.Position;
        }

        public void MoveAlways(int speed)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = speed;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _characterEntity.MoveOnce();
            _charactersRepository.Update(_characterEntity);
        }
    }
}

