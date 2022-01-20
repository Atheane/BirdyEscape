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
                throw new PositionException("Position X too large");
            if (value.X < XMin)
                throw new PositionException("Position X too small");
            if (value.Y > YMax)
                throw new PositionException("Position Y too large");
            if (value.Y < YMin)
                throw new PositionException("Position Y too small");
        }

    }
}
