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
            var snakeCoordsBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;

            if (snakeCoordsBuffer.Length == bounds.x * bounds.y)
            {
                SystemAPI.GetSingletonRW<GameStatus>().ValueRW.State = GameState.Won;
                Debug.Log("Won the match");
            }
        }
    }
}