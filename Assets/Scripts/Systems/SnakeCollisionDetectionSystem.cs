using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakeMovementSystem))]
    public partial struct SnakeCollisionDetectionSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var snakePosBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();
            var headPos = snakePosBuffer.ElementAt(snakePosBuffer.Length - 1).GridPosition;

            for (var i = 0; i < snakePosBuffer.Length - 1; i++)
            {
                var pos = snakePosBuffer[i].GridPosition;
                if (headPos.x == pos.x && headPos.y == pos.y)
                {
                    Debug.Log("Lost");
                    SystemAPI.GetSingletonRW<GameStatus>().ValueRW.Result = GameResult.Lost;
                    state.Enabled = false;
                }
            }
        }
    }
}