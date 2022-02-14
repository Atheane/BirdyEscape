using System;
using Libs.Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.ValueObjects
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
                throw new ImageException.ShouldNotBeNull();
        }
    }
}
