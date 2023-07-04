using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class SnakeDirectionAuthoring : MonoBehaviour
    {
        private class Baker : Baker<SnakeDirectionAuthoring>
        {
            public override void Bake(SnakeDirectionAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SnakeDirection());
            }
        }
    }
}