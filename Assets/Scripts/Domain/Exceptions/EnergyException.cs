using System;

namespace Domain.Exceptions
{
    public static class EnergyException
    {
        public class ShouldNotBeNegative : Exception
        {
            public ShouldNotBeNegative(string message = "") : base("Energy-ShouldNotBeNegative" + message) { }
        }
    }
}

