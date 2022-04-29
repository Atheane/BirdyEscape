using System;

namespace Domain.Exceptions
{
    public static class TileException
    {
        public class NotFound : Exception
        {
            public NotFound(string message = "") : base("Tile-NotFound" + message) { }
        }
        public class AlreadyExists : Exception
        {
            public AlreadyExists(string message = "") : base("Tile-AlreadyExists" + message) { }
        }
        public class MissingDirection : Exception
        {
            public MissingDirection(string message = "") : base("Tile-MissingDirection" + message) { }
        }
        public class MissingArrow : Exception
        {
            public MissingArrow(string message = "") : base("Tile-MissingArrow" + message) { }
        }
    }
}


