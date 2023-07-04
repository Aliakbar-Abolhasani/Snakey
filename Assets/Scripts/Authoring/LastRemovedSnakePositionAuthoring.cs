using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class LastRemovedSnakePositionAuthoring : MonoBehaviour
    {
        private class Baker : Baker<LastRemovedSnakePositionAuthoring>
        {
            public override void Bake(LastRemovedSnakePositionAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new LastRemovedSnakePosition());
            }
        }
    }
}