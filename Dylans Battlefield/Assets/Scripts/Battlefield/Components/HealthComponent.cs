using System;

using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Health : IComponentData
    {
        public int Value;
    }

    public class HealthComponent : ComponentDataProxy<Health> { }
}