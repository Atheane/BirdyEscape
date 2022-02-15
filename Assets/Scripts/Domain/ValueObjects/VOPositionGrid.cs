using System;
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

        public static (int X, int Y) ConvertToCoordinates((float X, float Y, float Z) position)
        {
            var coordX = (int)Math.Round(position.X, 0);
            var coordY = (int)Math.Round(position.Z, 0);
            return (coordX, coordY);
        }

        protected override void Validate((int X, int Y) value)
        {
            if (value.X > PositionGrid.X_MAX)
                throw new PositionException.TooLarge("X should be smaller than " + PositionGrid.X_MAX.ToString());
            if (value.X < PositionGrid.X_MIN)
                throw new PositionException.TooSmall("X should be higher than " + PositionGrid.X_MIN.ToString());
            if (value.Y > PositionGrid.Y_MAX)
                throw new PositionException.TooLarge("Y should be smaller than " + PositionGrid.Y_MAX.ToString());
            if (value.Y < PositionGrid.Y_MIN)
                throw new PositionException.TooSmall("Y should be higher than " + PositionGrid.Y_MIN.ToString());
        }
    }
}
