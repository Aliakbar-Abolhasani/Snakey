using System.Numerics;
using Snakey.Components;
using Unity.Entities;
using Vector3 = UnityEngine.Vector3;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakePositionInitializationSystem))]
    public partial struct SnakeRendererSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ManagedDirectory>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var lineRenderer = SystemAPI.ManagedAPI.GetSingleton<ManagedDirectory>().LineRenderer;
            var snakePositionBuffer = SystemAPI.GetSingletonBuffer<SnakePosition>();

            lineRenderer.positionCount = snakePositionBuffer.Length;
            for (var i = 0; i < snakePositionBuffer.Length; i++)
            {
                var pos = snakePositionBuffer[i].Position;
                lineRenderer.SetPosition(i, new Vector3(pos.x, pos.y, 0));
            }
        }
    }
}