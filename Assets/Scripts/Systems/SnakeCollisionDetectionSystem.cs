using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakeMovementSystem))]
    public partial struct SnakeCollisionDetectionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SnakeCoords>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var snakeCoordsBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();
            var head = snakeCoordsBuffer[^1].Coords;

            for (var i = 0; i < snakeCoordsBuffer.Length - 1; i++)
            {
                var c = snakeCoordsBuffer[i].Coords;
                if (head.x == c.x && head.y == c.y)
                {
                    Debug.Log("Lost");
                    SystemAPI.GetSingletonRW<GameStatus>().ValueRW.State = GameState.Lost;
                    state.Enabled = false;
                }
            }
        }
    }
}