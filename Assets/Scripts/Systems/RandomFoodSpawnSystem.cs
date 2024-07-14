using Snakey.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Utils;
using Random = Unity.Mathematics.Random;

namespace Snakey.Systems
{
    public partial struct RandomFoodSpawnSystem : ISystem
    {
        private Random _random;
        private Entity _foodEntity;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GridBounds>();
            state.RequireForUpdate<FoodSpawnData>();
            _random = Random.CreateFromIndex(1000);
        }

        public void OnUpdate(ref SystemState state)
        {
            var gameStatus = SystemAPI.GetSingleton<GameStatus>();
            if (gameStatus.State != GameState.Running || SystemAPI.Exists(_foodEntity))
            {
                return;
            }

            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;
            var snakeCoordsBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();

            var foodPrefab = SystemAPI.GetSingleton<FoodSpawnData>();

            _foodEntity = state.EntityManager.Instantiate(foodPrefab.Prefab);
            var transform = SystemAPI.GetComponentRW<LocalTransform>(_foodEntity);

            var occupiedCoords = new NativeList<int2>(5, Allocator.Temp);
            foreach (var snakeCoords in snakeCoordsBuffer)
            {
                occupiedCoords.Add(snakeCoords.Coords);
            }

            // food should not spawn on the edge of the bounds
            var randomCoords = _random.NextInt2(new int2(1, 1), bounds);
            while (occupiedCoords.Contains(randomCoords))
            {
                randomCoords = _random.NextInt2(new int2(1, 1), bounds);
            }

            transform.ValueRW.Position.xy = GridUtils.GridCoordsToWorldPosition(randomCoords, bounds);
        }
    }
}