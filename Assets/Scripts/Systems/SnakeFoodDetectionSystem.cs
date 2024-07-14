using Snakey.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Utils;

namespace Snakey.Systems
{
    [UpdateBefore(typeof(SnakeMovementSystem))]
    [UpdateBefore(typeof(SnakeRenderingSystem))]
    public partial struct SnakeFoodDetectionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SnakeCoords>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var snakePositionBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();

            var lastRemovedSnakeCoords = SystemAPI.GetSingletonRW<SnakeRuntimeData>().ValueRO.LastRemovedCoords;
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;

            var headPosition = GridUtils.GridCoordsToWorldPosition(snakePositionBuffer[^1].Coords, bounds);
            var tailPosition = GridUtils.GridCoordsToWorldPosition(snakePositionBuffer[0].Coords, bounds);

            foreach (var (food, foodEntity) in SystemAPI.Query<LocalTransform>().WithAll<Food>().WithEntityAccess())
            {
                if ((int)food.Position.x == headPosition.x && (int)food.Position.y == headPosition.y)
                {
                    var dir = lastRemovedSnakeCoords - tailPosition;
                    snakePositionBuffer.Insert(0, new SnakeCoords { Coords = tailPosition + dir });
                    ecb.DestroyEntity(foodEntity);
                    break;
                }
            }

            ecb.Playback(state.EntityManager);
        }
    }
}