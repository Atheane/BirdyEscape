using Libs.Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class VOPosition : ValueObject<(float X, float Y, float Z)>
    {
        private VOPosition((float X, float Y, float Z) value) : base(value) { }

        public static VOPosition Create((float X, float Y, float Z) value)
        {
            return new VOPosition(value);
        }

        protected override void Validate((float X, float Y, float Z) value)
        {
            if (value.X > PuzzleController.MAX.x)
                throw new PositionException.XTooLarge("X should be smaller than " + PuzzleController.MAX.x);
            else if (value.X < PuzzleController.MIN.x)
                throw new PositionException.XTooSmall("X should be higher than " + PuzzleController.MIN.x);
            else if (value.Z > PuzzleController.MAX.z)
                throw new PositionException.ZTooLarge("Z should be smaller than " + PuzzleController.MAX.z);
            else if (value.Z < PuzzleController.MIN.z)
                throw new PositionException.ZTooSmall("Z should be higher than " + PuzzleController.MIN.z);
        }

    }
}
