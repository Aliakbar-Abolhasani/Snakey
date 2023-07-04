using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class FoodAuthoring : MonoBehaviour
    {
        private class Baker : Baker<FoodAuthoring>
        {
            public override void Bake(FoodAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Food());
            }
        }
    }
}