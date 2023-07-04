using Unity.Entities;

namespace Snakey.Components
{
    public struct FoodSpawnData : IComponentData
    {
        public Entity Prefab;
    }
}