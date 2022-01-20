using Libs.Domain.ValueObjects;
using Domain.Characters.Exceptions;

namespace Domain.Characters.ValueObjects
{
    public class VOPosition : ValueObject<(float X, float Y)>
    {
        public const float XMin = -20.4f;
        public const float XMax = 0.0f;
        public const float YMin = -2.6f;
        public const float YMax = 30.7f;

        private VOPosition((float X, float Y) value) : base(value) { }

        public static VOPosition Create((float X, float Y) value)
        {
            return new VOPosition(value);
        }

        protected override void Validate((float X, float Y) value)
        {
            if (value.X > XMax)
                throw new PositionException.TooLarge("X should be smaller than " + XMax.ToString());
            if (value.X < XMin)
                throw new PositionException.TooSmall("X should be higher than " + XMin.ToString());
            if (value.Y > YMax)
                throw new PositionException.TooLarge("Y should be smaller than " + YMax.ToString());
            if (value.Y < YMin)
                throw new PositionException.TooSmall("Y should be higher than " + YMin.ToString());
        }

    }
}
