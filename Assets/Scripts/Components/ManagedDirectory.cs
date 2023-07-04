using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Snakey.Components
{
    public class ManagedDirectory : IComponentData
    {
        public List<LineRenderer> LineRenderers;
    }
}