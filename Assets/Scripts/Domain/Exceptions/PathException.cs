using System;

namespace Domain.Exceptions
{
    public static class PathException
    {
        public class ShouldNotBeNull : ArgumentNullException
        {
            public ShouldNotBeNull(string message = "") : base("Path-ShouldNotBeNull" + message) { }
        }
        public class ShouldNotBeEmpty : Exception
        {
            public ShouldNotBeEmpty(string message = "") : base("Path-ShouldNotBeEmpty" + message) { }
        }
    }
}


