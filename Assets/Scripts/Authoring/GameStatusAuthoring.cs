using Snakey.Components;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class GameStatusAuthoring : MonoBehaviour
    {
        private class Baker : Baker<GameStatusAuthoring>
        {
            public override void Bake(GameStatusAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new GameStatus());
            }
        }
    }
}