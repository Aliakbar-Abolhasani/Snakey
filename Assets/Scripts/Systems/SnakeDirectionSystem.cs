using Snakey.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Snakey.Systems
{
    [UpdateBefore(typeof(SnakeMovementSystem))]
    [UpdateAfter(typeof(SnakeInitializationSystem))]
    public partial struct SnakeDirectionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SnakeRuntimeData>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var snakeRuntimeData = SystemAPI.GetSingletonRW<SnakeRuntimeData>();
            var snakeCoordsBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();

            var head = snakeCoordsBuffer[^1];
            var behindHead = snakeCoordsBuffer[^2];

            var userInput = GetDirectionFromUserInput();
            if (userInput.x != 0 && head.Coords.x + userInput.x != behindHead.Coords.x)
            {
                snakeRuntimeData.ValueRW.MoveDirection = userInput;
            }
            else if (userInput.y != 0 && head.Coords.y + userInput.y != behindHead.Coords.y)
            {
                snakeRuntimeData.ValueRW.MoveDirection = userInput;
            }
        }

        private static int2 GetDirectionFromUserInput()
        {
            return new int2
            {
                x = (int)Input.GetAxisRaw("Horizontal"),
                y = (int)Input.GetAxisRaw("Vertical")
            };
        }
    }
}