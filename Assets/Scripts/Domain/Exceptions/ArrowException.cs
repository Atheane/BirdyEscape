using System;

namespace Domain.Exceptions
{
    public static class ArrowException
    {
        public class NotFound : Exception
        {
            public NotFound(string message = "") : base("Arrow-NotFound" + message) { }
        }
        public class AlreadyExists : Exception
        {
            public AlreadyExists(string message = "") : base("Arrow-AlreadyExists" + message) { }
        }
    }
}


