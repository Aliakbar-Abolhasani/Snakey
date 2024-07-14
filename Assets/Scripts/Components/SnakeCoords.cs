using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Components
{
    public struct SnakeCoords : IBufferElementData
    {
        public int2 Coords;
    }
}