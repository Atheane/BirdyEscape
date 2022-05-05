using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IUpdateTileArrowEffectCommand
    {
        Guid _tileId { get; }
        int _numberEffects { get; }
    }
    public class UpdateTileArrowEffectCommand : IMulticastMessage, IUpdateTileArrowEffectCommand
    {
        public Guid _tileId { get; }
        public int _numberEffects { get; }

        public UpdateTileArrowEffectCommand(Guid tileId, int numberEffects)
        {
            _tileId = tileId;
            _numberEffects = numberEffects;
        }
    }
}

