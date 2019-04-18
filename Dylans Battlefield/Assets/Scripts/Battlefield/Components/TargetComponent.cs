using System;

using Unity.Entities;

namespace Battlefield
{
    [Serializable]
    public struct Target : IComponentData
    {
        public Entity Value;
    }

    public class TargetComponent : ComponentDataProxy<Target> { }
}