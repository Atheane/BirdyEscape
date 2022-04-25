using System;
using UnityEngine;

namespace Frameworks.IO
{
    [Serializable]
    public class CharacterIO
    {
        public string _id;
        public float _totalDistance;

        public CharacterIO(Guid id, float totalDistance)
        {
            _id = id.ToString();
            _totalDistance = totalDistance;
        }
    }
}