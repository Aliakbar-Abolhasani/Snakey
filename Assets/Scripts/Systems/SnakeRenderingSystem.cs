using Snakey.Components;
using Unity.Entities;
using Utils;
using Vector3 = UnityEngine.Vector3;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakeInitializationSystem))]
    public partial struct SnakeRenderingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ManagedDirectory>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var managedDirectory = SystemAPI.ManagedAPI.GetSingleton<ManagedDirectory>();
            var snakePositionBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;

            var lineRenderers = managedDirectory.LineRenderers;
            lineRenderers.ForEach(r => r.positionCount = 0);

            var rendererIndex = 0;
            var rendererPosIndex = 0;
            var currentRenderer = managedDirectory.LineRenderers[rendererIndex];
            var previousGridPos = snakePositionBuffer[0].Coords;
            for (var i = 0; i < snakePositionBuffer.Length; i++)
            {
                var gridPos = snakePositionBuffer[i].Coords;
                var worldPos = GridUtils.GridCoordsToWorldPosition(gridPos, bounds);
                if (!GridUtils.IsNextToOrTheSame(gridPos, previousGridPos))
                {
                    currentRenderer = managedDirectory.LineRenderers[++rendererIndex];
                    rendererPosIndex = 0;
                }

                currentRenderer.positionCount++;
                currentRenderer.SetPosition(rendererPosIndex++, new Vector3(worldPos.x, worldPos.y, 0));
                previousGridPos = gridPos;
            }
        }
    }
}