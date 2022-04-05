using System;
using Libs.Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public sealed class VOEnergy : ValueObject<float>
    {
        private VOEnergy(float value) : base(value) { }

        public static VOEnergy Create(float value)
        {
            return new VOEnergy(value);
        }

        protected override void Validate(float value)
        {
            if (value < 0f)
                throw new EnergyException.ShouldNotBeNegative();
        }
    }
}
