using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakeCollisionDetectionSystem))]
    public partial struct WinDetectionSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var snakePosBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;

            if (snakePosBuffer.Length == bounds.x * bounds.y)
            {
                SystemAPI.GetSingletonRW<GameStatus>().ValueRW.Result = GameResult.Won;
                Debug.Log("Won the match");
            }
        }
    }
}