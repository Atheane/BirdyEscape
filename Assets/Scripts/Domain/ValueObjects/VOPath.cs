using System;
using Libs.Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public sealed class VOPath : ValueObject<string>
    {
        private VOPath(string value): base(value) { }

        public static VOPath Create(string value)
        {
            return new VOPath(value);
        }

        protected override void Validate(string value)
        {
            if (value == null)
                throw new PathException.ShouldNotBeNull();
            if (value == "")
                throw new PathException.ShouldNotBeNull();
            if (!value.Contains("Resources/"))
            {
                throw new PathException.ShouldContainResources();
            }
        }
    }
}
