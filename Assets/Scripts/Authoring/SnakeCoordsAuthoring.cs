using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class SnakeCoordsAuthoring : MonoBehaviour
    {
        private class Baker : Baker<SnakeCoordsAuthoring>
        {
            public override void Bake(SnakeCoordsAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddBuffer<SnakeCoords>(entity);
            }
        }
    }
}