using System;

using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Target : IComponentData
    {
        public Entity Value;
    }

    public class TargetComponent : ComponentDataProxy<Target> { }
}