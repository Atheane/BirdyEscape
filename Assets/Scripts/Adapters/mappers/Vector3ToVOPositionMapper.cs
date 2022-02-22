using Libs.Adapters;
using Domain.ValueObjects;
using UnityEngine;

namespace Adapters.Mappers
{
    public class Vector3ToVOPositionMapper : IMapper<VOPosition, Vector3>
{
        public VOPosition ToDomain(Vector3 position)
        {
            return VOPosition.Create((position[0], position[1], position[2]));
        }
    }
}
