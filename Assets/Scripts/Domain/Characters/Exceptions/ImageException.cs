using System;

namespace Domain.Characters.Exceptions
{
    public static class ImageException
    {
        public class ShouldNotBeNull : ArgumentNullException
        {
            public ShouldNotBeNull(string message = "") : base("Image-ShouldNotBeNull" + message) { }
        }
    }
}


