﻿using System;

namespace Characters.Exceptions
{
    public class PositionException : Exception
    {
        public PositionException(string message) : base("PositionException" + '-' + message) { }
    }
}
