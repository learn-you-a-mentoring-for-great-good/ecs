using System;

using Unity.Entities;

namespace Battlefield
{
    [Serializable]
    public struct RangedDamage : IComponentData
    {
        public int Value;
    }

    public class RangedDamageComponent : ComponentDataProxy<RangedDamage> { }
}