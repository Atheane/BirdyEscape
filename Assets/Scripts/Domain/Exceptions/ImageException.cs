using System;

namespace Domain.Exceptions
{
    public static class ImageException
    {
        public class ShouldNotBeNull : ArgumentNullException
        {
            public ShouldNotBeNull(string message = "") : base("Image-ShouldNotBeNull" + message) { }
        }
    }
}


