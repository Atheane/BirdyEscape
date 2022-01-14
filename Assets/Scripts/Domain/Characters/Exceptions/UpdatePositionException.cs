using System;

namespace Domain.Characters.Exceptions
{
    public class UpdatePositionException : InvalidOperationException
    {
        public UpdatePositionException(string message) : base("UpdatePositionException" + '-' + message) { }
    }
}
