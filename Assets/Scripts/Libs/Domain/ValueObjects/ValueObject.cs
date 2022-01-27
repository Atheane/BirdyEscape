using System;

namespace Libs.Domain.ValueObjects
{
    public abstract class ValueObject<T> : IValueObject<T>, IEquatable<T> where T : IEquatable<T>
    {
        public T Value { get; }

        protected ValueObject() { }
        protected ValueObject(T value)
        {
            this.Validate(value);

            this.Value = value;
        }

        protected abstract void Validate(T value);

        public bool Equals(T other)
        {
            return this.Value.Equals(other);
        }

        public static bool operator ==(ValueObject<T> a, T b) => a.Equals(b);

        public static bool operator !=(ValueObject<T> a, T b) => !a.Equals(b);
    }
}
