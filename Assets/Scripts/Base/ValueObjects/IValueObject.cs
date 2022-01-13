using System;

namespace Base.ValueObjects
{
    public interface IValueObject<T> : IEquatable<T> where T : IEquatable<T>
    {
        T Value { get; }
    }
}
