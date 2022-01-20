using System;

namespace Domain.Characters.Exceptions
{
    public static class PositionException
    {
        public class TooLarge : InvalidOperationException
        {
            public TooLarge(string message = "") : base("Position-TooLarge" + message) { }
        }
        public class TooSmall : InvalidOperationException
        {
            public TooSmall(string message = "") : base("Position-TooSmall" + message) { }
        }
        public class UpdateShouldChangeValue : InvalidOperationException
        {
            public UpdateShouldChangeValue(string message = "") : base("Position-UpdateShouldChangeValue" + message) { }
        }
    }
}
