using System;
using Libs.Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public sealed class VOEnergy : ValueObject<float>
    {
        public static float INIT_ENERGY = 100f;
        public static float ENERGY_PER_MINUTE = 0.2f;

        private VOEnergy(float value) : base(value) { }

        public static VOEnergy Create()
        {
            return new VOEnergy(INIT_ENERGY);
        }

        public static VOEnergy Load(float value)
        {
            return new VOEnergy(value);
        }

        public static VOEnergy Compute(float value, float distance, DateTime lastConnectionDate)
        {
            TimeSpan diff = DateTime.UtcNow - lastConnectionDate;
            var newValue = value - distance + ENERGY_PER_MINUTE * diff.Minutes;
            return new VOEnergy(newValue);
        }

        protected override void Validate(float value)
        {
            if (value < 0f)
                throw new EnergyException.ShouldNotBeNegative();
        }
    }
}
