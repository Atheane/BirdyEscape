using System;

namespace Domain.Exceptions
{
    public static class CoordinatesException
    {
        public class TooLarge : InvalidOperationException
        {
            public TooLarge(string message = "") : base("Coordinates-TooLarge" + message) { }
        }
        public class TooSmall : InvalidOperationException
        {
            public TooSmall(string message = "") : base("Coordinates-TooSmall" + message) { }
        }
        public class UpdateShouldChangeValue : InvalidOperationException
        {
            public UpdateShouldChangeValue(string message = "") : base("Coordinates-UpdateShouldChangeValue" + message) { }
        }
    }
}

