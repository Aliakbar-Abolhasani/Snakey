using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class FoodSpawnDataAuthoring : MonoBehaviour
    {
        public GameObject FoodPrefab;

        private class Baker : Baker<FoodSpawnDataAuthoring>
        {
            public override void Bake(FoodSpawnDataAuthoring authoring)
            {
                var foodEntity = GetEntity(authoring.FoodPrefab, TransformUsageFlags.Dynamic);
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new FoodSpawnData { Prefab = foodEntity });
            }
        }
    }
}