using Snakey.Components;
using Snakey.SyncObjects;
using Unity.Entities;
using UnityEngine;
using Utils;
using Vector3 = UnityEngine.Vector3;

namespace Snakey.Systems
{
    [UpdateAfter(typeof(SnakeInitializationSystem))]
    public partial class SnakeRenderingSystem : SystemBase
    {
        private Directory _directory;

        protected override void OnCreate()
        {
            RequireForUpdate<SnakeCoords>();
        }
        
        protected override void OnStartRunning()
        {
            _directory = Object.FindAnyObjectByType<Directory>();
        }

        protected override void OnUpdate()
        {

            var snakeCoordsBuffer = SystemAPI.GetSingletonBuffer<SnakeCoords>();
            var bounds = SystemAPI.GetSingleton<GridBounds>().Bounds;

            var lineRenderers = _directory.LineRenderers;
            lineRenderers.ForEach(r => r.positionCount = 0);

            var rendererIndex = 0;
            var rendererPosIndex = 0;
            var currentRenderer = _directory.LineRenderers[rendererIndex];
            var previousGridPos = snakeCoordsBuffer[0].Coords;
            for (var i = 0; i < snakeCoordsBuffer.Length; i++)
            {
                var gridPos = snakeCoordsBuffer[i].Coords;
                var worldPos = GridUtils.GridCoordsToWorldPosition(gridPos, bounds);
                if (!GridUtils.IsNextToOrTheSame(gridPos, previousGridPos))
                {
                    currentRenderer = _directory.LineRenderers[++rendererIndex];
                    rendererPosIndex = 0;
                }
            
                currentRenderer.positionCount++;
                currentRenderer.SetPosition(rendererPosIndex++, new Vector3(worldPos.x, worldPos.y, 0));
                previousGridPos = gridPos;
            }
        }
    }
}