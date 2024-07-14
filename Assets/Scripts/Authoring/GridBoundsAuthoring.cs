using Snakey.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Snakey.Authoring
{
    public class GridBoundsAuthoring : MonoBehaviour
    {
        public Vector2Int Bounds;

        private class Baker : Baker<GridBoundsAuthoring>
        {
            public override void Bake(GridBoundsAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new GridBounds { Bounds = new int2(authoring.Bounds.x, authoring.Bounds.y) });
            }
        }
    }
}