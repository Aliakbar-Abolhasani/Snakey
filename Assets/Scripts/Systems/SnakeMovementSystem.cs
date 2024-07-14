using Snakey.Components;
using Unity.Entities;
using UnityEngine;
using Utils;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakeInitializationSystem))]
    public partial struct SnakeMovementSystem : ISystem
    {
        private const float MoveDelay = 0.5f;
        private float _timer;

        public void OnCreate(ref SystemState state)
        {
            _timer = MoveDelay;

            state.RequireForUpdate<SnakeCoords>();
            state.RequireForUpdate<SnakeRuntimeData>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var gameStatus = SystemAPI.GetSingletonRW<GameStatus>();
            var snakeCoordsBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();
            var snakeData = SystemAPI.GetSingletonRW<SnakeRuntimeData>();
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;

            if (gameStatus.ValueRO.State > GameState.Running || snakeData.ValueRO.MoveDirection is { x: 0, y: 0 })
            {
                return;
            }

            gameStatus.ValueRW.State = GameState.Running;

            _timer += SystemAPI.Time.DeltaTime;
            if (_timer < MoveDelay)
            {
                return;
            }

            _timer = 0;

            var headCoords = snakeCoordsBuffer[^1];
            var newHeadCoords = headCoords.Coords + snakeData.ValueRO.MoveDirection;
            snakeCoordsBuffer.Add(new SnakeCoords { Coords = GridUtils.GetCoordsInsideGrid(newHeadCoords, bounds) });
            snakeData.ValueRW.LastRemovedCoords = snakeCoordsBuffer[0].Coords;
            snakeCoordsBuffer.RemoveAt(0);
        }
    }
}