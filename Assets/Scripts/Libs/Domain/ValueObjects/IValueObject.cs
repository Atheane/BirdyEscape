using System;

namespace Libs.Domain.ValueObjects
{
    public interface IValueObject<T> : IEquatable<T> where T : IEquatable<T>
    {
        T Value { get; }
    }
}
