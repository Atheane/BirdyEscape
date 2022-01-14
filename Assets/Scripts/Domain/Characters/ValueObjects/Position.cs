using Libs.Domain.ValueObjects;
using Domain.Characters.Exceptions;

namespace Domain.Characters.ValueObjects
{
    public class VOPosition : ValueObject<(double X, double Y)>
    {
        public const double XMin = 0.0;
        public const double XMax = 300.0;
        public const double YMin = 0.0;
        public const double YMax = 800.0;

        private VOPosition((double X, double Y) value) : base(value) { }

        public static VOPosition Create((double X, double Y) value)
        {
            return new VOPosition(value);
        }

        protected override void Validate((double X, double Y) value)
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
