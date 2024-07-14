using Snakey.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Systems
{
    public partial struct SnakeInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GridBounds>();
            state.RequireForUpdate<SnakeCoords>();
        }

        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;
            var buffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();
            buffer.Add(new SnakeCoords { Coords = new int2(bounds.x / 2 - 2, bounds.y / 2) });
            buffer.Add(new SnakeCoords { Coords = new int2(bounds.x / 2 - 1, bounds.y / 2) });
            buffer.Add(new SnakeCoords { Coords = new int2(bounds.x / 2, bounds.y / 2) });
        }
    }
}