using System;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.Types;
using Domain.Entities;


namespace Usecases
{
    public class MoveOnceCharacter : IUsecase<IMoveAlwaysCharacterCommand, VOPosition>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public ICharacterEntity _characterEntity;

        public MoveOnceCharacter(
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
            _characterEntity.MoveOnce();
            _charactersRepository.Update(_characterEntity);
            _domainEventDispatcher.Dispatch(_characterEntity);
            //MoveAlways(_characterEntity._speed);
            return _characterEntity._position;
        }

        //public void MoveAlways(int speed)
        //{
        //    System.Timers.Timer timer = new System.Timers.Timer();
        //    timer.Interval = speed;
        //    timer.Elapsed += Timer_Elapsed;
        //    timer.Start();
        //}

        //private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    _characterEntity.MoveOnce();
        //    _charactersRepository.Update(_characterEntity);
        //    _domainEventDispatcher.Dispatch(_characterEntity);
        //}
    }
}

