using Libs.Domain.ValueObjects;
using Domain.Exceptions;
using Domain.Constants;

namespace Domain.ValueObjects
{
    public class VOPosition : ValueObject<(float X, float Y, float Z)>
    {
        private VOPosition((float X, float Y, float Z) value) : base(value) { }

        public static VOPosition Create((float X, float Y, float Z) value)
        {
            return new VOPosition(value);
        }

        public static (float X, float Y, float Z) ConvertToPosition((int X, int Y) coordinates)
        {
            var posX = coordinates.X - 0.5f;
            var posY = Position.INIT_Y;
            var posZ = coordinates.Y - 0.5f;
            return (posX, posY, posZ);
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
