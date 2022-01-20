using System;

namespace Domain.Characters.Exceptions
{
    public static class CharacterException {
        public class NotFound : Exception
        {
            public NotFound(string message = "") : base("Character-NotFound" + message) { }
        }
        public class AlreadyExists : Exception
        {
            public AlreadyExists(string message = "") : base("Character-AlreadyExists" + message) { }

        }
    }
}

