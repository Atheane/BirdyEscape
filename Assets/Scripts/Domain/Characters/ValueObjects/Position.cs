using Libs.Domain.ValueObjects;
using Domain.Characters.Exceptions;
using Domain.Characters.Constants;

namespace Domain.Characters.ValueObjects
{
    public class VOPosition : ValueObject<(float X, float Y)>
    {
        private VOPosition((float X, float Y) value) : base(value) { }

        public static VOPosition Create((float X, float Y) value)
        {
            return new VOPosition(value);
        }

        protected override void Validate((float X, float Y) value)
        {
            if (value.X > Position.X_MAX)
                throw new PositionException.TooLarge("X should be smaller than " + Position.X_MAX.ToString());
            if (value.X < Position.X_MIN)
                throw new PositionException.TooSmall("X should be higher than " + Position.X_MIN.ToString());
            if (value.Y > Position.Y_MAX)
                throw new PositionException.TooLarge("Y should be smaller than " + Position.Y_MAX.ToString());
            if (value.Y < Position.Y_MIN)
                throw new PositionException.TooSmall("Y should be higher than " + Position.Y_MIN.ToString());
        }

    }
}
