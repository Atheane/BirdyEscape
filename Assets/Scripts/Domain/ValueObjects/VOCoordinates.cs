using System;
using Libs.Domain.ValueObjects;
using Domain.Constants;
using Domain.Exceptions;


namespace Domain.ValueObjects
{
    public class VOCoordinates : ValueObject<(int X, int Y)>
    {
        private VOCoordinates((int X, int Y) value) : base(value) { }

        public static VOCoordinates Create((int X, int Y) value)
        {
            return new VOCoordinates(value);
        }

        public static (int X, int Y) ConvertToCoordinates((float X, float Y, float Z) position)
        {
            var coordX = (int)Math.Round(position.X, 0);
            var coordY = (int)Math.Round(position.Z, 0);
            return (coordX, coordY);
        }

        protected override void Validate((int X, int Y) value)
        {
            if (value.X > Coordinates.X_MAX)
                throw new CoordinatesException.TooLarge("X should be smaller than " + Coordinates.X_MAX.ToString());
            if (value.X < Coordinates.X_MIN)
                throw new CoordinatesException.TooSmall("X should be higher than " + Coordinates.X_MIN.ToString());
            if (value.Y > Coordinates.Y_MAX)
                throw new CoordinatesException.TooLarge("Y should be smaller than " + Coordinates.Y_MAX.ToString());
            if (value.Y < Coordinates.Y_MIN)
                throw new CoordinatesException.TooSmall("Y should be higher than " + Coordinates.Y_MIN.ToString());
        }
    }
}
