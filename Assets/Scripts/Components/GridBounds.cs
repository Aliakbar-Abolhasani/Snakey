using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Components
{
    public struct GridBounds : IComponentData
    {
        public int2 Bounds;
    }
}