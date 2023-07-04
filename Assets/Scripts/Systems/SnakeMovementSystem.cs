using Snakey.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Utils;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakePositionInitializationSystem))]
    public partial struct SnakeMovementSystem : ISystem
    {
        private const float MoveDelay = 0.5f;
        private float _timer;

        public void OnCreate(ref SystemState state)
        {
            _timer = MoveDelay;

            state.RequireForUpdate<SnakePosition>();
            state.RequireForUpdate<SnakeDirection>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var gameStatus = SystemAPI.GetSingleton<GameStatus>();
            var snakePosBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();
            var snakeDir = SystemAPI.GetSingletonRW<SnakeDirection>();
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;

            if (gameStatus.Result == GameResult.Lost || snakeDir.ValueRO.MoveDirection is { x: 0, y: 0 })
            {
                return;
            }

            _timer += SystemAPI.Time.DeltaTime;
            if (_timer < MoveDelay)
            {
                return;
            }

            _timer = 0;

            var headPos = snakePosBuffer.ElementAt(snakePosBuffer.Length - 1);
            var newHeadPos = headPos.GridPosition + snakeDir.ValueRO.MoveDirection;
            snakePosBuffer.Add(new SnakePosition { GridPosition = GridUtils.GetCoordsInsideGrid(newHeadPos, bounds) });
            SystemAPI.GetSingletonRW<LastRemovedSnakePosition>().ValueRW.Position = snakePosBuffer.ElementAt(0).GridPosition;
            snakePosBuffer.RemoveAt(0);
        }
    }
}