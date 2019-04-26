using System;

using Unity.Entities;
using Unity.Mathematics;

namespace Battlefield
{
    [Serializable]
    public struct Target : IComponentData
    {
        public float3 Value;
    }

    public class TargetComponent : ComponentDataProxy<Target> { }
}