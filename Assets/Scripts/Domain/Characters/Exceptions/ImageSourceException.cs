using System;

namespace Domain.Characters.Exceptions
{
    public class ImageSourceException : ArgumentNullException
    {
        public ImageSourceException(string message) : base("ImageSourceException" + '-' + message) { }
    }
}
