using Libs.Usecases;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Characters.ValueObjects;
using Domain.Characters.Types;
using Domain.Characters.Entities;


namespace Usecases.Characters
{
    public class MoveAlwaysCharacterById : IUsecase<IMoveAlwaysCharacterByIdCommand, VOPosition>
    {
        public ICharactersRepository _charactersRepository;
        public ICharacterEntity _characterEntity;
        public MoveAlwaysCharacterById(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }
        public VOPosition Execute(IMoveAlwaysCharacterByIdCommand command)
        {
            _characterEntity = _charactersRepository.Find(command.CharacterId);
            _characterEntity.UpdateState(EnumCharacterState.MOVING);
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
            //to-do disptach events
            _charactersRepository.Update(_characterEntity);
        }
    }
}

