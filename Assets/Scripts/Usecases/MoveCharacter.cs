using System;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.Types;
using Domain.Entities;
using UnityEngine;
using Domain.Exceptions;


namespace Usecases
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
            MoveAlways(_characterEntity._speed);
            return _characterEntity._position;
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
            try
            {
                _characterEntity.MoveOnce();
            } catch(Exception er)
            {
                Debug.Log(er);
                EnumDirection newDirection = EnumDirection.LEFT;
                if (
                    er.GetType() == typeof(PositionException.XTooLarge) &&
                    _characterEntity._direction == EnumDirection.DOWN)
                {
                    newDirection = EnumDirection.LEFT;
                }
                if (
                    er.GetType() == typeof(PositionException.ZTooSmall) &&
                    _characterEntity._direction == EnumDirection.LEFT)
                {
                    newDirection = EnumDirection.UP;
                }
                if (
                    er.GetType() == typeof(PositionException.XTooSmall) &&
                    _characterEntity._direction == EnumDirection.UP)
                {
                    newDirection = EnumDirection.RIGHT;
                }
                if (
                    er.GetType() == typeof(PositionException.ZTooLarge) &&
                    _characterEntity._direction == EnumDirection.RIGHT)
                {
                    newDirection = EnumDirection.DOWN;
                }
                _characterEntity.UpdateDirection(newDirection);
                _charactersRepository.Update(_characterEntity);
                _domainEventDispatcher.Dispatch(_characterEntity);
            }
        }
    }
}

