using System;

namespace Frameworks.Exceptions
{
    public static class AppException
    {
        public class NullObject : Exception
        {
            public NullObject(string message = "") : base("ApplicationException-NullObject" + message) { }
        }
    }
}

