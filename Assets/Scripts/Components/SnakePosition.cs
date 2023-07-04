using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Components
{
    public struct SnakePosition : IBufferElementData
    {
        public int2 Position;
    }
}