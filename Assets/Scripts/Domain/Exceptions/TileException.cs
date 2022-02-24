using System;

namespace Domain.Exceptions
{
    public static class TileException
    {
        public class NotFound : Exception
        {
            public NotFound(string message = "") : base("Arrow-NotFound" + message) { }
        }
        public class AlreadyExists : Exception
        {
            public AlreadyExists(string message = "") : base("Arrow-AlreadyExists" + message) { }
        }
        public class MissingDirection : Exception
        {
            public MissingDirection(string message = "") : base("Arrow-MissingDirection" + message) { }
        }
    }
}


