using Snakey.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Snakey.Systems
{
    [UpdateBefore(typeof(SnakeMovementSystem))]
    [UpdateBefore(typeof(SnakeRendererSystem))]
    public partial struct SnakeFoodDetectionSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var snakePositionBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();

            var lastRemovedSnakePosition = SystemAPI.GetSingletonRW<LastRemovedSnakePosition>().ValueRW.Position;
            var tailPos = snakePositionBuffer[0].Position;

            foreach (var (foodTr, foodEntity) in SystemAPI.Query<LocalTransform>().WithAll<Food>().WithEntityAccess())
            {
                if ((int)foodTr.Position.x == tailPos.x && (int)foodTr.Position.y == tailPos.y)
                {
                    var dir = lastRemovedSnakePosition - tailPos;
                    snakePositionBuffer.Insert(0, new SnakePosition { Position = tailPos + dir });
                    ecb.DestroyEntity(foodEntity);
                    break;
                }
            }

            ecb.Playback(state.EntityManager);
        }
    }
}