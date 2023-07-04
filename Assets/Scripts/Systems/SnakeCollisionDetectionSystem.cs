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
            var headPos = snakePosBuffer.ElementAt(snakePosBuffer.Length - 1).Position;

            for (var i = 0; i < snakePosBuffer.Length - 1; i++)
            {
                var pos = snakePosBuffer[i].Position;
                if (headPos.x == pos.x && headPos.y == pos.y)
                {
                    SystemAPI.GetSingletonRW<GameStatus>().ValueRW.IsGameLost = true;
                    Debug.Log("Lost");
                    state.Enabled = false;
                }
            }
        }
    }
}