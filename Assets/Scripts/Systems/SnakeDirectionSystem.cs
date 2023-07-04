using Snakey.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Snakey.Systems
{
    [UpdateBefore(typeof(SnakeMovementSystem))]
    [UpdateAfter(typeof(SnakePositionInitializationSystem))]
    public partial struct SnakeDirectionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SnakeDirection>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var snakeDir = SystemAPI.GetSingletonRW<SnakeDirection>();
            var snakePosBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();

            var headPos = snakePosBuffer.ElementAt(snakePosBuffer.Length - 1);
            var behindHeadPos = snakePosBuffer.ElementAt(snakePosBuffer.Length - 2);

            var userDirInput = GetDirectionFromUserInput();
            if (userDirInput.x != 0 && headPos.Position.x + userDirInput.x != behindHeadPos.Position.x)
            {
                snakeDir.ValueRW.MoveDirection = userDirInput;
            }
            else if (userDirInput.y != 0 && headPos.Position.y + userDirInput.y != behindHeadPos.Position.y)
            {
                snakeDir.ValueRW.MoveDirection = userDirInput;
            }
        }

        private int2 GetDirectionFromUserInput()
        {
            var dir = new int2();

            if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }

            return dir;
        }
    }
}