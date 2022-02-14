using Libs.Domain.ValueObjects;
using Domain.Constants;
using Domain.Exceptions;


namespace Domain.ValueObjects
{
    public class VOPositionGrid : ValueObject<(int X, int Y)>
    {
        private VOPositionGrid((int X, int Y) value) : base(value) { }

        public static VOPositionGrid Create((int X, int Y) value)
        {
            return new VOPositionGrid(value);
        }

        protected override void Validate((int X, int Y) value)
        {
            if (value.X > PositionGrid.X_MAX)
                throw new PositionException.TooLarge("X should be smaller than " + PositionGrid.X_MAX.ToString());
            if (value.X < PositionGrid.X_MIN)
                throw new PositionException.TooSmall("X should be higher than " + PositionGrid.X_MIN.ToString());
            if (value.Y > PositionGrid.Y_MAX)
                throw new PositionException.TooLarge("Z should be smaller than " + PositionGrid.Y_MAX.ToString());
            if (value.Y < PositionGrid.Y_MIN)
                throw new PositionException.TooSmall("Z should be higher than " + PositionGrid.Y_MIN.ToString());
        }
    }
}
