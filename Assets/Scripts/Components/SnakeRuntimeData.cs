using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Components
{
    public struct SnakeRuntimeData : IComponentData
    {
        public int2 MoveDirection;
        public int2 LastRemovedCoords;
    }
}