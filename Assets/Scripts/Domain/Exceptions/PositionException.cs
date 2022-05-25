using System;

namespace Domain.Exceptions
{
    public static class PositionException
    {
        public class OutOfBoundaries : InvalidOperationException
        {
            public OutOfBoundaries(string message = "") : base("Position-OutOfBoundaries" + message) { }
        }
        public class XTooLarge : InvalidOperationException
        {
            public XTooLarge(string message = "") : base("Position-XTooLarge" + message) { }
        }
        public class ZTooLarge : InvalidOperationException
        {
            public ZTooLarge(string message = "") : base("Position-ZTooLarge" + message) { }
        }
        public class XTooSmall : InvalidOperationException
        {
            public XTooSmall(string message = "") : base("Position-XTooSmall" + message) { }
        }
        public class ZTooSmall : InvalidOperationException
        {
            public ZTooSmall(string message = "") : base("Position-ZTooSmall" + message) { }
        }
        public class UpdateShouldChangeValue : InvalidOperationException
        {
            public UpdateShouldChangeValue(string message = "") : base("Position-UpdateShouldChangeValue" + message) { }
        }
    }
}
