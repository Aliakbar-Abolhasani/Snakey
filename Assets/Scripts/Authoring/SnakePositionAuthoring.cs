using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class SnakePositionAuthoring : MonoBehaviour
    {
        private class Baker : Baker<SnakePositionAuthoring>
        {
            public override void Bake(SnakePositionAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddBuffer<SnakePosition>(entity);
            }
        }
    }
}