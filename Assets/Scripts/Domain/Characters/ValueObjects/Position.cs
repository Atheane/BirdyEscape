using Libs.Domain.ValueObjects;
using Domain.Characters.Exceptions;
using Domain.Characters.Constants;

namespace Domain.Characters.ValueObjects
{
    public class VOPosition : ValueObject<(float X, float Y, float Z)>
    {
        private VOPosition((float X, float Y, float Z) value) : base(value) { }

        public static VOPosition Create((float X, float Y, float Z) value)
        {
            return new VOPosition(value);
        }

        protected override void Validate((float X, float Y, float Z) value)
        {
            if (value.X > Position.X_MAX)
                throw new PositionException.TooLarge("X should be smaller than " + Position.X_MAX.ToString());
            if (value.X < Position.X_MIN)
                throw new PositionException.TooSmall("X should be higher than " + Position.X_MIN.ToString());
            if (value.Z > Position.Z_MAX)
                throw new PositionException.TooLarge("Z should be smaller than " + Position.Z_MAX.ToString());
            if (value.Z < Position.Z_MIN)
                throw new PositionException.TooSmall("Z should be higher than " + Position.Z_MIN.ToString());
        }

    }
}
