using System;

namespace Domain.Exceptions
{
    public static class LevelException
    {
        public class NotFound : Exception
        {
            public NotFound(string message = "") : base("Level-NotFound" + message) { }
        }
        public class AlreadyExists : Exception
        {
            public AlreadyExists(string message = "") : base("Level-AlreadyExists" + message) { }
        }
    }
}

