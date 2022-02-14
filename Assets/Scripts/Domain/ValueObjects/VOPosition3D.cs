using Libs.Domain.ValueObjects;
using Domain.Exceptions;
using Domain.Constants;

namespace Domain.ValueObjects
{
    public class VOPosition3D : ValueObject<(float X, float Y, float Z)>
    {
        private VOPosition3D((float X, float Y, float Z) value) : base(value) { }

        public static VOPosition3D Create((float X, float Y, float Z) value)
        {
            return new VOPosition3D(value);
        }

        protected override void Validate((float X, float Y, float Z) value)
        {
            if (value.X > Position3D.X_MAX)
                throw new PositionException.TooLarge("X should be smaller than " + Position3D.X_MAX.ToString());
            if (value.X < Position3D.X_MIN)
                throw new PositionException.TooSmall("X should be higher than " + Position3D.X_MIN.ToString());
            if (value.Z > Position3D.Z_MAX)
                throw new PositionException.TooLarge("Z should be smaller than " + Position3D.Z_MAX.ToString());
            if (value.Z < Position3D.Z_MIN)
                throw new PositionException.TooSmall("Z should be higher than " + Position3D.Z_MIN.ToString());
        }

    }
}
