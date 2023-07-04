using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Components
{
    public struct SnakeDirection : IComponentData
    {
        public int2 MoveDirection;
    }
}