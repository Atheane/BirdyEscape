using System;
using Libs.Domain.ValueObjects;
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

        protected override void Validate((int X, int Y) value)
        {
            var xMAX = PuzzleController.MAX.x - PuzzleController.MIN.x;
            var yMAX = PuzzleController.MAX.z - PuzzleController.MIN.z;

            if (value.X > xMAX)
                throw new CoordinatesException.TooLarge("X should be smaller than " + xMAX);
            if (value.X < 0)
                throw new CoordinatesException.TooSmall("X should be higher than " + 0);
            if (value.Y > yMAX)
                throw new CoordinatesException.TooLarge("Y should be smaller than " + yMAX);
            if (value.Y < 0)
                throw new CoordinatesException.TooSmall("Y should be higher than " + 0);
        }
    }
}
