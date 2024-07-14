using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class SnakeRuntimeDataAuthoring : MonoBehaviour
    {
        private class Baker : Baker<SnakeRuntimeDataAuthoring>
        {
            public override void Bake(SnakeRuntimeDataAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SnakeRuntimeData());
            }
        }
    }
}