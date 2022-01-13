using System;
using Base.ValueObjects;
using Characters.Exceptions;

namespace Characters.ValueObjects
{
    public sealed class VOImage : ValueObject<string>
    {
        private VOImage(string value): base(value) { }

        public static VOImage Create(string value)
        {
            return new VOImage(value);
        }

        protected override void Validate(string value)
        {
            if (value == null)
                throw new ImageSourceException("Image should have source value");
        }
    }
}
