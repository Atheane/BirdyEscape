using System;

namespace Adapters.Exceptions
{
    public static class AppException
    {
        public class NullObject : Exception
        {
            public NullObject(string message = "") : base("ApplicationException-NullObject" + message) { }
        }
        public class FileNotFound : Exception
        {
            public FileNotFound(string message = "") : base("ApplicationException-FileNotFound" + message) { }
        }
    }
}

