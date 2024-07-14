using Snakey.Components;
using Snakey.SyncObjects;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Authoring
{
    public class ManagedDirectoryAuthoring : MonoBehaviour
    {
        private class Baker : Baker<ManagedDirectoryAuthoring>
        {
            public override void Bake(ManagedDirectoryAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                if (Directory.Instance != null)
                {
                    AddComponentObject(entity, new ManagedDirectory
                    {
                        LineRenderers = Directory.Instance.LineRenderers
                    });
                }
            }
        }
    }
}