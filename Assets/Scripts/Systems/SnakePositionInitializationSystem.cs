using Snakey.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace Snakey.Systems
{
    public partial struct SnakePositionInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GridBounds>();
            state.RequireForUpdate<SnakePosition>();
        }

        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;
            var snakePosBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();
            snakePosBuffer.Add(new SnakePosition { GridPosition = new int2(bounds.x / 2 - 2, bounds.y / 2) });
            snakePosBuffer.Add(new SnakePosition { GridPosition = new int2(bounds.x / 2 - 1, bounds.y / 2) });
            snakePosBuffer.Add(new SnakePosition { GridPosition = new int2(bounds.x / 2, bounds.y / 2) });
        }
    }
}