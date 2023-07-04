using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Components
{
    public struct LastRemovedSnakePosition : IComponentData
    {
        public int2 Position;
    }
}