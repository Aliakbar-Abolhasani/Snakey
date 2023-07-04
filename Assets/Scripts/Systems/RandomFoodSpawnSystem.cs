using Snakey.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Snakey.Systems
{
    public partial struct RandomFoodSpawnSystem : ISystem
    {
        private const float SpawnDelay = 3f;
        private float _timer;
        private Random _random;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GridBounds>();
            state.RequireForUpdate<FoodSpawnData>();
            _random = Random.CreateFromIndex(1000);
        }

        public void OnUpdate(ref SystemState state)
        {
            var gameStatus = SystemAPI.GetSingleton<GameStatus>();
            if (gameStatus.Result != GameResult.Undetermined)
            {
                return;
            }

            _timer += SystemAPI.Time.DeltaTime;
            if (_timer < SpawnDelay)
            {
                return;
            }

            _timer = 0;

            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;
            var snakePosBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();

            var foodPrefab = SystemAPI.GetSingleton<FoodSpawnData>();

            var instance = state.EntityManager.Instantiate(foodPrefab.Prefab);
            var tr = SystemAPI.GetComponentRW<LocalTransform>(instance);

            var occupiedPositions = new NativeList<int2>(5, Allocator.Temp);
            foreach (var snakePosition in snakePosBuffer)
            {
                occupiedPositions.Add(snakePosition.Position);
            }

            foreach (var foodTr in SystemAPI.Query<LocalTransform>().WithAll<Food>())
            {
                occupiedPositions.Add((int2)foodTr.Position.xy);
            }

            var randomGridPos = _random.NextInt2(0, bounds);
            while (occupiedPositions.Contains(randomGridPos))
            {
                randomGridPos = _random.NextInt2(0, bounds);
            }

            tr.ValueRW.Position.xy = randomGridPos;
        }
    }
}